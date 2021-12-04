using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using GameStore.Data;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
namespace GameStore.Controllers
{
  public class Store : Controller
  {

    private readonly  DBContext _context;

            public Store(DBContext context)
        {
            _context = context;
        }

    //แสดงสินค้าทั้งหมดและหมวดหมู่ GET: /Store/
        public async Task<IActionResult> Index()
        {
            return View(await _context.Game.ToListAsync());
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