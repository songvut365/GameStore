using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


    //แดสงรายละเอียดของเกมนั้นๆ GET: /Store/Game?id=5
    public async Task<IActionResult> Game(int? id)
    {
      var game = await _context.Game.FirstOrDefaultAsync(m => m.Id == id);

      return View(game);
    }
  }
}