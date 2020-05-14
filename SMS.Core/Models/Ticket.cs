
using System;
using System.Text.Json.Serialization;

namespace SMS.Core.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Issue { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }

        // Foreign key relating to Student ticket owner
        [JsonIgnore] public int StudentId { get; set; }

        // Navigation property to navigate to the student
        [JsonIgnore] public Student Student { get; set; }
    } 
}
