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
  public class ManagementController : Controller
  {
    private readonly  DBContext _context;

    public ManagementController(DBContext context)
    {
        _context = context;
    }
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
      return View();
    }



    //เพิ่มสินค้าในสต๊อก GET: /Management/Add/ 
    public IActionResult Add()
    {
      return View();
    }


    //แก้ไขสินค้าในสต๊อกตาม id GET: /Management/Edit?id=1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["Id"] = id;
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Youtube,Main_Image,Image1,Image2,Image3,Developer,Name,Type,Detail,Price,Amount")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
    private bool GameExists(int id)
    {
    return _context.Game.Any(e => e.Id == id);
    }
  }
}