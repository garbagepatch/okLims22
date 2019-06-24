using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using okLims.Data;
using okLims.Models;

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
    }
}
    