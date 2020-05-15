
using System.Collections.Generic;

namespace SMS.Core.Dtos
{
    public class StudentDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int Age { get; set; }
        
        public string Email { get; set; }
        public double Grade { get; set; }
        public IList<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    }
}