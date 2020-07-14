
namespace SMS.Core.Dtos
{
    public class UserDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        // convert role to int
        public int Role { get; set; }
        public string Token { get; set; }
    }
}