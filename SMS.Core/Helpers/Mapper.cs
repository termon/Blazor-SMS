
using System;
using System.Linq;
using SMS.Core.Models;

namespace SMS.Core.Dtos
{
    public static class Mapper
    {
        public static Ticket ToTicket(this TicketDto dto)
        {
            if (dto == null) return null;
            return new Ticket {
                Id = dto.Id,
                StudentId = dto.StudentId,
                Issue = dto.Issue,
                CreatedOn = dto.CreatedOn,
                Active = dto.Active
            };
        }

        public static TicketDto ToDto(this Ticket t)
        {
            if (t == null) return null;
            return new TicketDto {
                Id = t.Id,
                StudentId = t.StudentId,
                Issue = t.Issue,
                CreatedOn = t.CreatedOn,
                Active = t.Active
            };
        } 

       public static Student ToStudent(this StudentDto s)
        {
            if (s == null) return null;
            return new Student {
                Id = s.Id,
                Name = s.Name,
                Course = s.Course,
                Age = s.Age,
                Email = s.Email,
                PhotoUrl = s.PhotoUrl,
                Profile = new Profile { Grade = s.Grade }
            };
        } 

        public static StudentDto ToDto(this Student s)
        {
            if (s == null) return null;
            return new StudentDto {
                Id = s.Id,
                Name = s.Name,
                Course = s.Course,
                Age = s.Age,
                Email = s.Email,
                PhotoUrl = s.PhotoUrl,
                Grade = s.Profile != null ? s.Profile.Grade : 0.0,
                Tickets = s.Tickets.Select(t => t.ToDto()).ToList()
            };
        } 

        public static string ToPrintable(this StudentDto s)  
        {      
            return $"Dto Id:{s.Id} Name:{s.Name} Course:{s.Course} Age:{s.Age} Email: {s.Email} Grade: {s.Grade} ";
        }
 
    }


}