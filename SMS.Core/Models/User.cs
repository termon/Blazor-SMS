using System;
namespace SMS.Core.Models {
    public enum Role { Admin, Manager, Guest }

    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        // used to store jwt auth token 
        public string Token { get; set; }
    }
}
