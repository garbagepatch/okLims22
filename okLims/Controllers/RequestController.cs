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
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public RequestController(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
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
        [AllowAnonymous]
      public IActionResult SubmitRequest(int id = 0)
        {
            if (id == 0)
            {
                return View(new Request());
            }else 
            return View(_context.Request.Find(id)); 
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitRequest([Bind("RequestId, RequesterEmail, Start, End, LaboratoryId, SizeID, FilterID, ControllerID, SpecialDetails, StatusID")] Request request)
        {
            if (ModelState.IsValid)
            {

                if (request.RequestId == 0)
                {
                    _context.Add(request);
                }
                else
                    _context.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            return View(request);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EmailerOnCompletion( int id)
        {
            if (ModelState.IsValid)
            {

                Request request = _context.Request.SingleOrDefault(x => x.RequestId.Equals(id));

                if (request.StatusID == 2)
                {

                    _emailSender.SendEmailOnCompletion(request.RequesterEmail);
                }
            }
            return View();
        }
    }

}
    