using System;
using Xunit;
using SMS.Data.Services;
using SMS.Core.Models;

namespace SMS.Test
{
    public class StudentServiceTest
    {
        private readonly IStudentService svc;

        public StudentServiceTest()
        {
            // general arrangement
            svc = new StudentService();

            // ensure database is empty before each test
            svc.Initialise();
        }

        [Fact]
        public void AddStudent_WhenNone_ShouldSetAllProperties()
        {
            // act 
            var o = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            // retrieve student saved 
            var s = svc.GetStudent(o.Id);

            // assert - that student and their profile is not null
            Assert.NotNull(s);
            Assert.NotNull(s.Profile);


            // now assert that the properties were set properly
            Assert.Equal(s.Id, s.Id);
            Assert.Equal("XXX", s.Name);
            Assert.Equal("xxx@email.com", s.Email);
            Assert.Equal("Computing", s.Course);
            Assert.Equal(20, s.Age);
            Assert.Equal(0, s.Profile.Grade);
        }

        [Fact]
        public void UpdateStudent_ThatExists_ShouldSetAllProperties()
        {
            // arrange - create test student
            var o = svc.AddStudent("ZZZ", "zzz@email.com", "Maths", 30, 100);
                       
            // act - update test student
            var s = new Student {
                Name = "XXX",           
                Email = "xxx@email.com",
                Course = "Computing",
                Age = 20,
                Profile = new Profile { Grade = 0 }
            };
            s = svc.UpdateStudent(o.Id, s);

            // assert
            Assert.NotNull(s);
            Assert.NotNull(s.Profile);

            // now assert that the properties were set properly           
            Assert.Equal("XXX", s.Name);
            Assert.Equal("xxx@email.com", s.Email);
            Assert.Equal("Computing", s.Course);
            Assert.Equal(20, s.Age);
            Assert.Equal(0, s.Profile.Grade);
        }
 

        [Fact] 
        public void GetAllStudents_WhenNone_ShouldReturn0()
        {
            // act 
            var students = svc.GetAllStudents();
            var count = students.Count;

            // assert
            Assert.Equal(0, count);
        }


        [Fact]
        public void GetStudents_With2Added_ShouldReturn2()
        {
            // arrange
            var s1 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var s2 = svc.AddStudent("YYY", "yyy@email.com", "Engineering", 23, 0);

            // act
            var students = svc.GetAllStudents();
            var count = students.Count;

            // assert
            Assert.Equal(2, count);
        }


        [Fact] 
        public void GetStudent_WhenNone_ShouldReturnNull()
        {
            // act 
            var student = svc.GetStudent(1); // non existent student

            // assert
            Assert.Null(student);
        }



        [Fact] 
        public void GetStudent_WhenAdded_ShouldReturnStudent()
        {
            // act 
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);

            var ns = svc.GetStudent(s.Id);

            // assert
            Assert.NotNull(ns);
            Assert.Equal(s.Id, ns.Id);
        }


        [Fact]
        public void DeleteStudent_ThatExists_ShouldReturnTrue()
        {
            // act 
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var deleted = svc.DeleteStudent(s.Id);

            // assert
            Assert.True(deleted);
        }


        [Fact]
        public void DeleteStudent_ThatDoesntExist_ShouldNotWork()
        {
            // act 	
            var deleted = svc.DeleteStudent(0);

            // assert
            Assert.False(deleted);
        }

        [Fact]
        public void CloseTicket_WhenNonExistent_ShouldReturnNull()
        {
            // act 	
            var ticket = svc.CloseTicket(0);

            // assert
            Assert.Null(ticket);
        }


        [Fact] // Q
        public void RecalculateStudentGrade_ForNewStudentWithNoModules_ShouldBeZero()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);

            var us = svc.RecalculateStudentGrade(s.Id);

            // assert
            Assert.Equal(0.0, us.Profile.Grade);
        }

        [Fact] // Q
        public void RecalculateStudentGrade_ForNewStudentWithModules_ShouldBeZero()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var m1 = svc.AddModule("M1");
            var m2 = svc.AddModule("M1");
            svc.AddStudentToModule(s.Id, m1.Id);
            svc.AddStudentToModule(s.Id, m2.Id);

            s = svc.RecalculateStudentGrade(s.Id);

            // assert
            Assert.Equal(0.0, s.Profile.Grade);
        }

        [Fact] 
        public void AddStudentToModule_WhereNotAlreadyTakingModule_ShouldWork()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var m = svc.AddModule("XXXX");

            // act
            var sm = svc.AddStudentToModule(s.Id, m.Id);
            var r = svc.GetStudent(s.Id);
            Assert.Equal(1, r.StudentModules.Count);
        }

        [Fact] // --- AddTicket to NewStudent should be saved
        public void AddTicket_NewStudent_ShouldBeOpenAndSaved()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
         
            // act
            var t = svc.CreateTicket(s.Id, "Dummy Ticket 1");
           
            // assert
            Assert.Equal("Dummy Ticket 1",t.Issue);
            Assert.True(t.Active); 
        }

        [Fact] // --- GetOpenTickets When two added should return two 
        public void GetOpenTickets_WhenTwoAdded_ShouldReturnTwo()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var t1 = svc.CreateTicket(s.Id, "Dummy Ticket 1");
            var t2 = svc.CreateTicket(s.Id, "Dummy Ticket 2");

            // act
            var open = svc.GetOpenTickets();

            Assert.Equal(2,open.Count);                        
        }


        [Fact] // --- AddStudent Duplicate Test
        public void AddStudent_WhenDuplicate_ShouldReturnNull()
        {
            // act 
            var s1 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var s2 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);

            // assert
            Assert.NotNull(s1);
            Assert.Null(s2);
        }

        

        [Fact] // --- AddStudentToModule duplicate test
        public void AddStudentToModule_WhenAlreadyTakingModule_ShouldReturNull()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var m = svc.AddModule("XXXX");

            // act
            var sm1 = svc.AddStudentToModule(s.Id, m.Id);
            var sm2 = svc.AddStudentToModule(s.Id, m.Id); // duplicate

            var r = svc.GetStudent(s.Id);

            // assert
            Assert.Null(sm2);
            Assert.Equal(1, r.StudentModules.Count);
        }

       

        [Fact] // --- CloseTicket for already closed ticket should return null 
        public void CloseTicket_ForAlreadyClosedTicket_ShouldReturnNull()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var t = svc.CreateTicket(s.Id, "Dummy Ticket");

            // act
            var r1 = svc.CloseTicket(t.Id);
            var r2 = svc.CloseTicket(t.Id);

            Assert.NotNull(r1);            
            Assert.Null(r2);
            
        }
  

        [Fact]  // -- RecalculateStudent grade
        public void RecalculateStudentGrade_WhenModuleMarksUpdated_ShouldWork()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0);
            var m1 = svc.AddModule("Maths");
            var m2 = svc.AddModule("Physics");
            svc.AddStudentToModule(s.Id, m1.Id);
            svc.AddStudentToModule(s.Id, m2.Id);

            // act
            var sm1 = svc.UpdateStudentModuleMark(s.Id, m1.Id, 50.0);
            var sm2 = svc.UpdateStudentModuleMark(s.Id, m2.Id, 60.0);         
            s = svc.RecalculateStudentGrade(s.Id);

            // assert
            Assert.Equal(55.0, s.Profile.Grade);
        }

        [Fact]
        public void UpdateStudent_ExistingStudentWithAgePlusOne_ShouldWork()
        {
            // arrange
            var s = svc.AddStudent("Clare", "Computing", "clare@gmail.com", 21, 0);

            // act
            s.Age = s.Age + 1;
            svc.UpdateStudent(s.Id, s);
            var su = svc.GetStudent(s.Id);

            // asssert
            Assert.Equal(s.Age, su.Age);
        }

        
    }
}
