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

    public IActionResult Information() 
    {
      return View();
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

    //สร้างออเดอร์สั่งซื้อสินค้า POST: /Cart/Create
    [HttpPost]
    public async Task<IActionResult> Create(JsonResult order) 
    {
      if (ModelState.IsValid) {
        _context.Add(order);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Success));
      }  
      return RedirectToAction(nameof(Failed));
    }
  }
}