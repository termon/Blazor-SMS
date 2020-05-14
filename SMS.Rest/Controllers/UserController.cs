using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using SMS.Data.Services;
using SMS.Core.Dtos;
using SMS.Rest.Helpers;
using SMS.Core.Models;
using System.Collections.Generic;

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
        return Ok(_service.GetAllUsers());
    }
    

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest login)
    {
        var user = _service.Authenticate(login.EmailAddress, login.Password);            
        if (user == null)
        {
            return Ok(new LoginResult { Successful = false, Error = "Username and/or password are invalid." });
        }
        return Ok( JwtHelper.SignJwtToken(user, _configuration));
    }
    
    [HttpPost("register")]
    public IActionResult Post([FromBody]RegisterRequest model)
    {
        var user = _service.RegisterUser(model.Name,model.EmailAddress,model.Password, model.Role);       
        if (user == null)
        {  
            return Ok(new RegisterResult { Successful = false, Error =  "Email Address is already registered. Please use another." });
        }

        return CreatedAtAction(nameof(Login), new RegisterResult { Successful = true });
    }

    [HttpGet("verify/{email}")]
    public IActionResult VerifyEmailAvailabl(string email)
    {
        return Ok(_service.GetUserByEmailAddress(email)==null);
    }

}