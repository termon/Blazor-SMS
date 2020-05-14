
using System;
using System.Text.Json.Serialization;

namespace SMS.Core.Models
{
    // Class representing a table in our database
    public class Profile
    {
        public int Id { get; set; }
        
        public double Grade { get; set; }
 
        // Foreign Key attribute - convention is it begins with the name of the
        // related model and ends with Id
        [JsonIgnore] public int StudentId { get; set; }

        // Navigation property to navigate to the related Student
        [JsonIgnore] public Student Student { get; set; }

    }
}