using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
  public class Cart : Controller
  {

    //แสดงสินค้าในตระกร้า GET: /Cart/
    public IActionResult Index()
    {
      return View();
    }

    //แสดงการสั่งซื้อสำเร็จ GET: /Cart/Success
    public IActionResult Success() {
      return View();
    }

  }
}