using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using Syncfusion.EJ2;

namespace okLims.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestController(ApplicationDbContext context)
        {
            _context = context;
           
        }
            public IActionResult Index()
            {
            var request = _context.Request.ToListAsync();
            ViewBag.request = request;
            return View();
            }
        public IActionResult Detail(int id)
        {
            Request request = _context.Request.SingleOrDefault(x => x.RequestId.Equals(id));
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }
        public IActionResult RequestCalendar()
        {
           ViewBag.Request = _context.Request.Take(100).ToList();
            return View();
        }
    }
}
    