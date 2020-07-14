using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using SMS.Data.Services;
using SMS.Core.Dtos;
using SMS.Rest.Helpers;

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
        return Ok( _service.GetAllUsers());
    }    

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        var user = _service.Authenticate(login.EmailAddress, login.Password);            
        if (user == null)
        {          
            return Unauthorized( new ErrorResponse { Message = "Username and/or password are invalid."} );
        }
        var dto = user.ToDto();
        dto.Token = JwtHelper.SignJwtToken(user, _configuration);
        return Ok( dto );
    }
    
    [HttpPost("register")]
    public IActionResult Post([FromBody]RegisterDto model)
    {
        var user = _service.RegisterUser(model.Name,model.EmailAddress,model.Password, model.Role);       
        if (user == null)
        {  
            return BadRequest(new ErrorResponse { Message = "Error creating user" } );
        }
        return CreatedAtAction(nameof(Login), user);
     }

    [HttpGet("verify/{email}/{id?}")]
    public IActionResult VerifyEmailAvailable(string email, int? id)
    {
        return Ok(_service.GetUserByEmailAddress(email, id)==null);
    }
}