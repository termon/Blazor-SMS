using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Core.Dtos;
using SMS.Core.Helpers;
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
        [Authorize]
        public IActionResult GetAll()
        {
            var students =  _service.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]   
        [Authorize]    
        public IActionResult Get(int id)
        {
            var student =  _service.GetStudent(id); 
            var dto = student.ToDto();
            if (student == null)
            {
                return NotFound(Problem($"student {id} not found", statusCode: 404).Value);
            }
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult create(StudentDto s)
        {
  
            var student = _service.AddStudent(s.Name, s.Email,  s.Course,  s.Age, s.PhotoUrl, s.Grade);
            if (student == null)
            {  
                return BadRequest(Problem($"Problem creating student", statusCode: 400 ).Value);
            }
            return CreatedAtAction(
                nameof(Get), 
                new { Id = student.Id }, 
                student.ToDto()
            );
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult update(int id, StudentDto m)
        {
            var student = m.ToStudent();
            var updatedStudent = _service.UpdateStudent(id, student); 
            if (updatedStudent == null)
            {  
                return BadRequest( Problem($"Problem updating user {id}", statusCode: 400 ).Value);
            }          
            return Ok(updatedStudent.ToDto());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult delete(int id)
        {
            if (_service.DeleteStudent(id))
            {
                return Ok();
            }
            return NotFound(Problem($"Student {id} not found", statusCode: 404 ).Value);
        }

        [HttpGet("verify/{email}/{id?}")]
        public IActionResult VerifyEmailAvailable(string email, int? id)
        {
            return Ok(_service.GetStudentByEmailAddress(email, id)==null);
        }
    }
}
