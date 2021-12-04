using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameStore.Controllers
{
 
      
    public class RandomGame : Controller
    {
            private readonly  DBContext _context;
             

            public RandomGame(DBContext context)
        {
            _context = context;
        }

            //เอาเกมส์มาแสดงแบบสุ่ม GET: /RandomGame/
        public async Task<IActionResult> Index()
        {
      

            var game = await _context.Game.ToListAsync();
            var random = new Random();
            var gameList = new List<Game>();
            //เอาเกมส์มาแสดงแบบสุ่มแบบไม่ซ้ำ
            while (gameList.Count < 5)
            {
                var gameIndex = random.Next(game.Count);
                if (!gameList.Contains(game[gameIndex]))
                {
                    gameList.Add(game[gameIndex]);
                }
            }

            return Json(gameList);
 
        }

        
        
    }
}