using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Dtos
{
    public class LoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
       
    }
}
