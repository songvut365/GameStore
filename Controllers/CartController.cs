using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
      if (ModelState.IsValid) {
        List<Cart> carts = await _context.Cart.ToListAsync();
        
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
          }
          else {
            return RedirectToAction(nameof(Failed));
          }
        }

        //3. clear game in cart
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