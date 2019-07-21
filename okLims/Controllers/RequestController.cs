using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using okLims.Data;
using okLims.Extensions;
using okLims.Models;
using okLims.Services;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Schedule;

namespace okLims.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Services.IEmailSender _emailSender;
        private readonly ILogger _logger;
        public RequestController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        Services.IEmailSender emailSender,
        ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
           
        }
        public IActionResult Index()
        {

            ViewBag.data = new string[] { "Submitted", "Completed" };
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EmailerOnCompletion( int id)
        {
           Request request = _context.Request.SingleOrDefault(x => x.RequestId.Equals(id));

            if (request.RequestStatus.Status == "Completed")
            {
               
                await _emailSender.SendEmailOnCompletion(request.RequesterEmail);
            }
            return View();
        }
    }

}
    