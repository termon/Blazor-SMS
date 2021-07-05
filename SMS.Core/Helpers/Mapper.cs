
using System.Linq;
using SMS.Core.Models;

namespace SMS.Core.Dtos
{
    public static class Mapper
    {
        public static UserDto ToDto(this User u)
        {
            return new UserDto {
                Id = u.Id,
                Name = u.Name,
                EmailAddress = u.EmailAddress,
                // password is removed for security
                Role = u.Role,
                Token = u.Token
            };
        }

        public static User ToUser(this UserDto dto)
        {
            return new User {
                Id = dto.Id,
                Name = dto.Name,
                EmailAddress = dto.EmailAddress,
                // password is removed for security
                Role = (Role)dto.Role,
                Token = dto.Token
            };
        }

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
                Profile = new Profile { Grade = s.Grade },
                Tickets = s.Tickets.Select(t => t.ToTicket()).ToList(),
                StudentModules = s.StudentModules.Select(m => m.ToStudentModule()).ToList()
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
                Tickets = s.Tickets.Select(t => t.ToDto()).ToList(),
                StudentModules = s.StudentModules.Select(m => m.ToDto()).ToList()
            };
        } 

        public static StudentModuleDto ToDto(this StudentModule m)
        {
            if (m == null) return null;
            return new StudentModuleDto {
                Id = m.Id,
                Mark = m.Mark,
                StudentId = m.StudentId,
                ModuleId = m.ModuleId,
                Title = m.Module.Title
            };
        }

        public static StudentModule ToStudentModule(this StudentModuleDto m)
        {
            if (m == null) return null;
            return new StudentModule {
                Id = m.Id,
                Mark = m.Mark,
                StudentId = m.StudentId,
                ModuleId = m.ModuleId
            };
        }

        public static string ToPrintable(this StudentDto s)  
        {      
            return $"Dto Id:{s.Id} Name:{s.Name} Course:{s.Course} Age:{s.Age} Email: {s.Email} Grade: {s.Grade} ";
        }
 
    }


}