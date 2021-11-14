using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace GameStore.Controllers
{
  public class ManagementController : Controller
  {

    //หน้าล็อคอิน GET: /Management/
    public IActionResult Index()
    {
      return View();
    }


    //แสดงรายการสินค้าในสต๊อก GET: /Management/List
    public IActionResult List()
    {
      return View();
    }


    //เพิ่มสินค้าในสต๊อก GET: /Management/Add/ 
    public IActionResult Add()
    {
      return View();
    }


    //แก้ไขสินค้าในสต๊อกตาม id GET: /Management/Edit?id=1
    public IActionResult Edit(int id)
    {
      ViewData["Id"] = id;

      return View();
    }
  }
}