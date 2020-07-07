using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Core.Models;
using SMS.Core.Dtos;
using SMS.Data.Services;
using SMS.Rest.Models;
using System.Collections.Generic;

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
            return Ok(ResponseApi<IList<Student>>.Ok(students));
        }

        [HttpGet("{id}")]       
        public IActionResult Get(int id)
        {
            var student =  _service.GetStudent(id); 
            var dto = student.ToDto();
            if (student == null)
            {
                return NotFound(ResponseApi<object>.NotFound($"student {id} not found"));
            }
            return Ok(ResponseApi<StudentDto>.Ok(dto));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult create(StudentDto s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseApi<object>.BadRequest(ModelState));
            }

            var student = _service.AddStudent(s.Name, s.Email,  s.Course,  s.Age, s.PhotoUrl, s.Grade);
            if (student == null)
            {  
                return BadRequest(ResponseApi<object>.BadRequest("Error creating student")); //Ok(new RegisterResult { Successful = false, Error =  "Email Address is already registered. Please use another." });
            }
            return CreatedAtAction(
                nameof(Get), 
                new { Id = student.Id }, 
                ResponseApi<StudentDto>.Created(student.ToDto())
            );
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
            if (updatedStudent == null)
            {  
                return BadRequest(ResponseApi<object>.BadRequest($"Problem updating user {id}"));
            }          
            return Ok(ResponseApi<StudentDto>.Ok(updatedStudent.ToDto()));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public IActionResult delete(int id)
        {
            if (_service.DeleteStudent(id))
            {
                return Ok( ResponseApi<StudentDto>.Ok(null, $"Student {id} deleted"));
            }
            return NotFound(ResponseApi<StudentDto>.NotFound($"Student {id} not found"));
        }
    }
}
