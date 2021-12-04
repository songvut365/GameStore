using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    public IActionResult Index()
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
  }
}