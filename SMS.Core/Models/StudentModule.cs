using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SMS.Core.Models
{
    public class StudentModule
    {
        public int Id { get; set; }
        public double Mark {get; set; }

        // Foreign key for related Student model
        [JsonIgnore] public int StudentId { get; set; }
        [JsonIgnore] public Student Student { get; set; }

        // Foreign key for related Module model
        [JsonIgnore] public int ModuleId { get; set; }
        [JsonIgnore] public Module Module { get; set; }
    }
}
