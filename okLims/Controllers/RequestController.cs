using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Schedule;

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
        public IActionResult RequestCalendar()
        {
            ViewBag.appointments = _context.Request.ToListAsync() ;
            List<ScheduleView> viewOption = new List<ScheduleView>()
    {
        new ScheduleView { Option = Syncfusion.EJ2.Schedule.View.Day },
        new ScheduleView { Option = Syncfusion.EJ2.Schedule.View.Week },
        new ScheduleView { Option = Syncfusion.EJ2.Schedule.View.WorkWeek }
    };
            ViewBag.view = viewOption;
            return View();
            
        }
      public IActionResult SubmitRequest()
        {
            return View(); 
        }
    }
}
    