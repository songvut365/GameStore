using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace GameStore.Controllers
{
  public class Cart : Controller
  {

    //แสดงสินค้าในตระกร้า GET: /Cart/
    public IActionResult Index()
    {
      return View();
    }

  }
}