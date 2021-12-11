using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;

namespace GameStore.Controllers
{
  public class Store : Controller
  {
    private readonly  DBContext _context;

    public Store(DBContext context) {
      _context = context;
    }

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
      ViewData["search"] = name;

          var game = _context.Game.Where(entity => entity.Name.ToLower().Contains(name.ToLower()))
            .ToList();

      return View(game);
    }


<<<<<<< HEAD
    //แดสงรายละเอียดของเกมนั้นๆ GET: /Store/Game?game=CallOfDuty
    public IActionResult Game(string name)
=======
    //แดสงรายละเอียดของเกมนั้นๆ GET: /Store/Game?id=5
    public async Task<IActionResult> Game(int? id)
>>>>>>> 886f48c10a5b5a91e31027d92848610b7a4c9cf9
    {
      var game = await _context.Game.FirstOrDefaultAsync(m => m.Id == id);

      return View(game);
    }
  }
}