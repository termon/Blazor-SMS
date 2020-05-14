using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Core.Models;
using SMS.Core.Dtos;
using SMS.Data.Services;

namespace SMS.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IStudentService _service;

        // Inject Service
        public TicketController(IStudentService service) {
            this._service = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var tickets=  _service.GetAllTickets();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var t =  _service.GetTicket(id); 
            if (t == null)
            {
                return NotFound();
            }
            
            return Ok(t);           
        }

        [HttpPost] 
        [Authorize(Roles="Admin")]   
        public IActionResult create(CreateTicketRequest m)
        {
            var ticket = _service.CreateTicket(m.StudentId,m.Issue);
            if (ticket != null)
            {
                return CreatedAtAction(nameof(Get), new { Id = ticket.Id }, ticket);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]   
        public IActionResult delete(int id)
        {
            var ok = _service.DeleteTicket(id);
            if (ok)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
