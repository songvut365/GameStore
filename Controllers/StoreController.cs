using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace GameStore.Controllers
{
  public class Store : Controller
  {

    //แสดงสินค้าทั้งหมดและหมวดหมู่ GET: /Store/
    public IActionResult Index()
    {
      return View();
    }

    //แสดงสินค้าตามประเภทแบบไดนามิก GET: /Store/Type?name=Action
    public IActionResult Type(string name)
    {
      ViewData["Type"] = name;

      return View();
    }


    //แดสงรายละเอียดของเกมนั้นๆ GET: /Store/Game?=CallOfDuty
    public IActionResult Game(string name)
    {
      ViewData["Name"] = name;

      return View();
    }
  }
}