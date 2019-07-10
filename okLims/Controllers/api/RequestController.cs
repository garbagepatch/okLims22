using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Models;
using okLims.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Request")]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;


        public RequestController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;

        }
        // GET: api/Request
        [HttpGet]
        public async Task<IActionResult> GetRequest()
        {
            List<Request> Items = await _context.Request.ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
    

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Request result = await _context.Request
                .Where(x => x.RequestId.Equals(id))
                .Include(x => x.RequestLines)
                .FirstOrDefaultAsync();
            return Ok(result);
        }

        private void UpdateRequest(int RequestId)
        {
            try
            {
                Request Request = new Request();
                Request = _context.Request
                    .Where(x => x.RequestId.Equals(RequestId))
                    .FirstOrDefault();
                if (Request != null)
                {
                    List<RequestLine> lines = new List<RequestLine>();
                    lines = _context.RequestLine.Where(x => x.RequestId.Equals(RequestId)).ToList();
                    //update master data by its lines                                       
                    _context.Update(Request);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = payload.value;            
            _context.Request.Add(Request);
            _context.SaveChanges();
            this.UpdateRequest(Request.RequestId);
            return Ok(Request);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = payload.value;
            _context.Request.Update(Request);
            _context.SaveChanges();
            return Ok(Request);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = _context.Request
                .Where(x => x.RequestId == (int)payload.key)
                .FirstOrDefault();
            _context.Request.Remove(Request);
            _context.SaveChanges();
            return Ok(Request);
        }
    }
}