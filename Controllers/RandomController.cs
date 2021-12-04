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
    public class Random : Controller
    {
            private readonly  DBContext _context;

            public Random(DBContext context)
        {
            _context = context;
        }

            //เอาเกมส์มาแสดงแบบสุ่ม GET: /Random/
        public async Task<IActionResult> Index()
        {
            return Json(await _context.Game.ToListAsync());
        }
        
    }
}