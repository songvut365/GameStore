using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using GameStore.Data;
using GameStore.Models;
using System.Collections.Generic;
using System;

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
    public async Task<IActionResult> Type(string type)
    {
      ViewData["Type"] = type;

    var game = await _context.Game.Where(g => g.Type == type).ToListAsync();

      return View(game);
    }

        //แสดงสินค้าตามการค้นหา GET: /Store/Search?name=Action
    public async Task<IActionResult> Search(string name)
    {
      ViewData["Name"] = name;

    var game = await _context.Game.Where(g => g.Name == name).ToListAsync();

      return View(game);
    }


    //แดสงรายละเอียดของเกมนั้นๆ GET: /Store/Game?game=CallOfDuty
    public IActionResult Game(string name)
    {
      ViewData["Name"] = name;

      return View();
    }
  }
}