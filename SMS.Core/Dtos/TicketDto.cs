using System;
using System.ComponentModel.DataAnnotations;
using SMS.Core.Models;

namespace SMS.Core.Dtos
{
    public class TicketDto 
    {
        public int Id { get; set;}
        public int StudentId { get; set; }
        public string Issue { get; set; }
        public DateTime CreatedOn { get; set;}
        public bool Active { get; set; }
    }
}