
using System.Collections.Generic;
using SMS.Core.Models;

namespace SMS.Core.Dtos
{
    public class StudentDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int Age { get; set; }        
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public double Grade { get; set; }
        public IList<TicketDto> Tickets { get; set; } = new List<TicketDto>();
        public IList<StudentModuleDto> StudentModules { get; set; } = new List<StudentModuleDto>();
    }
}