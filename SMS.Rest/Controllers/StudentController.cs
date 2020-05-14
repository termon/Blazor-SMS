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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        // Inject Service 
        public StudentController(IStudentService service) {
            this._service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var students =  _service.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]       
        public IActionResult Get(int id)
        {
            var student =  _service.GetStudent(id); 
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult create(StudentDto s)
        {
            var student = _service.AddStudent(s.Name, s.Email,  s.Course,  s.Age, s.Grade);
            if (student != null)
            {
                return CreatedAtAction(nameof(Get), new { Id = student.Id }, student);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult update(int id, StudentDto m)
        {
            var student = new Student {
                Id = id,
                Name = m.Name,
                Age = m.Age,
                Course = m.Course,
                Email = m.Email,
                Profile = new Profile { Grade = m.Grade }
            };
            var updatedStudent = _service.UpdateStudent(id, student);           
            if (updatedStudent != null)
            {
                return Ok(updatedStudent);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult delete(int id)
        {
            var ok = _service.DeleteStudent(id);
            if (ok)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
