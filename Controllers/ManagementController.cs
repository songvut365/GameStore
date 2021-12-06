using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using GameStore.Models;

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

    //แสดงรายการคำสั่งซื้อ GET: /Management/Order
    public IActionResult Order()
    {
      List<Order> NewOrder = new List<Order>();
      NewOrder.Add(new Order{ Id =1, Game_Id = 1, Game_Amount = 1, Price_Total=250, Name="สานตอก", LastName="อะสะบะละ", Phone=0881234567, Email="santok@buaUrai.com"});
      NewOrder.Add(new Order{ Id =2, Game_Id = 2, Game_Amount = 1, Price_Total=150, Name="ซงวุด", LastName="อะสะบะละ", Phone=0887654321, Email="songvut@buaUrai.com"});
      return View(NewOrder);
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