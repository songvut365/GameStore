using System;    
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;

namespace GameStore.Controllers
{
  public class CartController : Controller
  {
    private readonly DBContext _context;

    public CartController(DBContext context)
    {
      _context = context;
    }

    //แสดงสินค้าในตระกร้า GET: /Cart/
    public async Task<IActionResult> Index()
    {
      Dictionary<Game, Cart> carts = new Dictionary<Game, Cart>();

      List<Cart> temps = await _context.Cart.ToListAsync<Cart>();
      foreach(var item in temps) {
        Game game = await _context.Game.FirstOrDefaultAsync(m => m.Id == item.gameId);
        carts.Add(game, item);
      }

      return View(carts);
    }

    //เพิ่มสินค้าลงในตะกร้า
    public async Task<IActionResult> AddToCart(int id) {
      Cart cart = new Cart();
      cart.gameId = id;

      Game game = await _context.Game.FirstOrDefaultAsync(m => m.Id == id);
      
      Cart hasInCart = await _context.Cart.FirstOrDefaultAsync(m => m.gameId == id);
    

      if(hasInCart == null) {
        cart.count = 1;
        cart.totalPrice = game.Price;
        _context.Cart.Add(cart);
      }
      else {
        hasInCart.count += 1;
        hasInCart.totalPrice = hasInCart.count * game.Price;
        _context.Cart.Update(hasInCart);
      }

      await _context.SaveChangesAsync();

      return RedirectToAction("Index", "Cart");
    }
    
    //ลบสินค้าออกจากตะกร้า
    public async Task<IActionResult> RemoveFromCart(int id) {
      Cart cart = new Cart();
      cart.gameId = id;

      Game game = await _context.Game.FirstOrDefaultAsync(m => m.Id == id);
      
      Cart hasInCart = await _context.Cart.FirstOrDefaultAsync(m => m.gameId == id);

      if(hasInCart == null) {
        cart.count = 1;
        cart.totalPrice = game.Price;
        _context.Cart.Add(cart);
      }
      else {
        if(hasInCart.count > 1){
          hasInCart.count -= 1;
          hasInCart.totalPrice = hasInCart.count * game.Price;
          _context.Cart.Update(hasInCart);
        }
        else {
          _context.Cart.Remove(hasInCart);
        }
      }

      await _context.SaveChangesAsync();

      return RedirectToAction("Index", "Cart");
    }

    //หน้ากรอกข้อมูลยืนยันการสั่งซื้อสินค้า GET: /Cart/Information
    public IActionResult Information() 
    {
      return View();
    }

    //สร้างออเดอร์สั่งซื้อสินค้า
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order) 
    {
      //email setting
      SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("gamestore.swstudio@gmail.com", "gamestore.swstudio1150");
        var senderMail = new MailAddress("gamestore.swstudio@gmail.com", "Game Store");
        var receiverEmail = new MailAddress(order.Email, "Receiver");

        //mail body
        var mailBody = new StringBuilder();
        mailBody.AppendLine("<div style='font-family: Arial, Helvetica, sans-serif; width: 700px; padding: 1rem; border: 1px solid #212121; border-radius: 8px;'>");
        mailBody.AppendLine("<h1 style='margin:0; padding:0; color: #212121;'>Game Store</h1><p style='color: #212121;'>ขอบคุณที่ใช้บริการ Game Store คุณสามารถนำ CD-Key ไปลงทะเบียนได้ตามผู้ให้บริการที่คุณต้องการ หากคุณมีคำถามโปรดติดต่อ Call Center</p>");
        mailBody.AppendLine("<div><table style='border: 1px solid #212121; margin-bottom: 1rem; width: 49%; float: left;'>");
        mailBody.AppendLine("<tr style='background-color: #212121; color: white;'><th>Billing Information</th></tr>");
        mailBody.AppendLine($"<tr><td>ชื่อ: {order.Name +' '+order.LastName}</td></tr><tr><td>เบอร์โทร: {order.Phone}</td></tr><tr><td>อีเมล: {order.Email}</td></tr>");
        mailBody.AppendLine("</table><table style='border: 1px solid #212121; margin-bottom: 1rem; width: 49%; float: right;'>");
        mailBody.AppendLine("<tr style='background-color: #212121; color: white;'><th>Payment Status</th></tr>");
        mailBody.AppendLine("<tr><td style='color: green;'>Success</td></tr><tr><td>&nbsp;</td></tr> <tr><td>&nbsp;</td></tr></table></div><table style='border: 1px solid #212121; margin-bottom: 1rem; width: 100%;'>");
        mailBody.AppendLine("<tr style='background-color: #212121; color: white;'><th >No.</th><th>Game</th><th>CD-Key</th><th>ราคา</th></tr>");  

      if (ModelState.IsValid) {
        List<Cart> carts = await _context.Cart.ToListAsync();
        
        int counter = 1;
        decimal allPrice = 0;
        //1. foreach all game in cart
        foreach(Cart cart in carts) {
          Order temp = new Order();
          temp.Name = order.Name;
          temp.LastName = order.LastName;
          temp.Phone = order.Phone;
          temp.Email = order.Email;
          temp.Game_Id = cart.gameId;
          temp.Game_Amount = cart.count;
          temp.Price_Total = cart.totalPrice;
          
          //2. check amount game can't be less than 1
          Game game = await _context.Game.FirstOrDefaultAsync(g => g.Id == cart.gameId);
          if(game.Amount > temp.Game_Amount) {
            game.Amount -= temp.Game_Amount;
            await _context.AddAsync(temp);
            await _context.SaveChangesAsync();
            
            for(int i=0; i<temp.Game_Amount; i++) {
              mailBody.AppendLine($"<tr><td>{counter}</td><td>{game.Name}</td><td>{Guid.NewGuid().ToString()}</td><td>{game.Price}</td></tr>");
              counter++;
            }
            allPrice += temp.Price_Total;
          }

          //if game not enough
          else {
            return RedirectToAction(nameof(Failed));
          }
        }


        mailBody.AppendLine($"<tr style='background-color: #212121; color: white;' ><td>&nbsp;</td><td>&nbsp;</td><td style='text-align: center;'>ราคารวม</td><td>{allPrice}</td></tr>");
        mailBody.AppendLine("</table></div>");

        //3. Send mail
        using (var message = new MailMessage(senderMail, receiverEmail)) {
          message.Subject = "คำสั่งซื้อจาก Game Store";
          message.Body = mailBody.ToString();
          message.IsBodyHtml = true;
          smtp.Send(message);
        }

        //4. clear game in cart
        _context.RemoveRange(_context.Cart);
        await _context.SaveChangesAsync();        
        return RedirectToAction(nameof(Success));
      }  
      return RedirectToAction(nameof(Failed));
    }

    //แสดงการสั่งซื้อสำเร็จ GET: /Cart/Success
    public IActionResult Success()
    {
      return View();
    }

    //แสดงการสั่งซื้อไม่สำเร็จ GET: /Cart/Error
    public IActionResult Failed() {
      return View();
    }

  }
}