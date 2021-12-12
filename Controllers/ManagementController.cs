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

using Microsoft.AspNetCore.Http;
using System.IO;

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
    public async Task<IActionResult> List()
    {
        List<Game> gameList = await _context.Game.ToListAsync(); 
        return View(gameList);
    }

    //แสดงรายการคำสั่งซื้อ GET: /Management/Order
    public async Task<IActionResult> Order()
    {
        List<Order> NewOrder = await _context.Order.ToListAsync();
        return View(NewOrder);
    }

<<<<<<< HEAD
    // POST: Game/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var game = await _context.Game.FindAsync(id);
        _context.Game.Remove(game);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(List));
=======
    // Upload file on server
    public async Task<bool> UploadFile(IFormFile file , string pathimg, string newfilename)
    {
        string path = "";
        bool iscopied = false;
        try
        {
            if (file.Length>0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + pathimg));
                using (var filestream = new FileStream(Path.Combine(path, newfilename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }
                iscopied = true;
            }
            else
            {
                iscopied = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return iscopied;
>>>>>>> main
    }

    //เพิ่มสินค้าในสต๊อก GET: /Management/Add/ 
    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([Bind("Id,Youtube,Main_Image,Image1,Image2,Image3,Developer,Name,Type,Detail,Price,Amount,img")] Game game , IFormFile[] img)
    {
        if (ModelState.IsValid)
        {
            _context.Add(game);
            try {
                await UploadFile(img[0],"img_main", game.Main_Image);
                await UploadFile(img[1],"img_1", game.Image1);
                await UploadFile(img[2],"img_2", game.Image2);
                await UploadFile(img[3],"img_3", game.Image3);    
            }
            catch {
                TempData["msg"] = "กรุณาเพื่มรูปให้ครบทุกช่อง";
                return View(game);
            }         
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData["msg"] = "กรุณากรอกข้อมูลให้ครบถ้วน";
        return View(game);
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
    public async Task<IActionResult> Edit(int id, [Bind("Id,Youtube,Main_Image,Image1,Image2,Image3,Developer,Name,Type,Detail,Price,Amount,img")] Game game , IFormFile[] img)
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
                try {
                    await UploadFile(img[0],"img_main", game.Main_Image);
                    await UploadFile(img[1],"img_1", game.Image1);
                    await UploadFile(img[2],"img_2", game.Image2);
                    await UploadFile(img[3],"img_3", game.Image3);    
                }
                catch
                { }                  
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
        TempData["msg"] = "กรุณากรอกข้อมูลให้ครบถ้วน";
        return View(game);
    }
    private bool GameExists(int id)
    {
    return _context.Game.Any(e => e.Id == id);
    }
  }
}