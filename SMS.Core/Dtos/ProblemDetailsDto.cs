using System.Collections.Generic;

// https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/

namespace SMS.Core.Dtos
{
    public class ProblemDetailsDto 
    {          
        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public IEnumerable<object> Errors { get; set;}
    }
   
}
