using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SMS.Core.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Title { get; set; }
            
        // Navigation property
        [JsonIgnore] ICollection<StudentModule> StudentModules { get; set; }
    }
}
