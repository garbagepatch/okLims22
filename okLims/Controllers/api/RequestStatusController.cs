using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/RequestStatus")]
    public class RequestStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestStatusController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetRequestStatus()
       {
            List<RequestStatus> Items = await _context.RequestStatus.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<RequestStatus> payload)
        {
            RequestStatus RequestStatus = payload.value;
           _context.RequestStatus.Add(RequestStatus);
            _context.SaveChanges();
            return Ok(RequestStatus);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<RequestStatus> payload)
        {
            RequestStatus RequestStatus = payload.value;
          _context.RequestStatus.Update(RequestStatus);
            _context.SaveChanges();
            return Ok(RequestStatus);
        }

       [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<RequestStatus> payload)
        {
            RequestStatus RequestStatus = _context.RequestStatus
                .Where(x => x.StatusID == (int)payload.key)
                .FirstOrDefault();
           _context.RequestStatus.Remove(RequestStatus);
            _context.SaveChanges();
            return Ok(RequestStatus);

        }
        
    }
}