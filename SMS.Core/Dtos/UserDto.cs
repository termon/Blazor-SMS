
using System.Text.Json.Serialization;
using SMS.Core.Models;

namespace SMS.Core.Dtos
{
    public class UserDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        // apply json enum converter
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}