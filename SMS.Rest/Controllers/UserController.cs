using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using SMS.Data.Services;
using SMS.Core.Dtos;
using SMS.Rest.Helpers;
using SMS.Core.Models;
using System.Collections.Generic;
using SMS.Rest.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IStudentService _service;

    // Inject Services
    public UserController(IStudentService service, IConfiguration configuration)
    {
        _configuration = configuration;
        _service = service;
    }

    [HttpGet]
    public IActionResult UserList()
    {
        return Ok( ResponseApi<IList<User>>.Ok(_service.GetAllUsers()));
    }    

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        var user = _service.Authenticate(login.EmailAddress, login.Password);            
        if (user == null)
        {          
            return Unauthorized( ResponseApi<object>.NotAuthorised("Username and/or password are invalid.") );
        }
        return Ok( ResponseApi<string>.Ok(JwtHelper.SignJwtToken(user, _configuration), "login successful") );
    }
    
    [HttpPost("register")]
    public IActionResult Post([FromBody]RegisterRequest model)
    {
        var user = _service.RegisterUser(model.Name,model.EmailAddress,model.Password, model.Role);       
        if (user == null)
        {  
            return BadRequest(ResponseApi<object>.BadRequest("Error creating user"));
        }
        return CreatedAtAction(nameof(Login), ResponseApi<User>.Created(user));
     }

    [HttpGet("verify/{email}")]
    public IActionResult VerifyEmailAvailable(string email)
    {
        return Ok(_service.GetUserByEmailAddress(email)==null);
    }

}