using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Dtos
{
    public class CreateTicketRequest 
    {
        public int StudentId { get; set; }
        public string Issue { get; set; }
    }
}