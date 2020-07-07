using System;
using System.Collections.Generic;

using SMS.Core.Models;

namespace SMS.Data.Services
{
    // This interface describes the operations that a StudentService class should implement
    public interface IStudentService
    {
        // Re-initialise the Database - only to be used during development 
        void Initialise();

        // ------------- User Management -------------------
        User Authenticate(string email, string password);
        User RegisterUser(string name, string email, string password, Role role);
        User GetUserByEmailAddress(string email, int? id=null);
        IList<User> GetAllUsers();
        User GetUser(int id);

        // ---------------- Student Management --------------
        IList<Student> GetAllStudents();
        Student GetStudent(int id);
        Student GetStudentByEmail(string email, int? id=null);
        Student AddStudent(string name, string email, string course, int age, string photoUrl, double grade);
        bool DeleteStudent(int id);
        Student UpdateStudent(int id, Student updated);
        Student RecalculateStudentGrade(int studentId);     

        // -------------- Module Management -----------------
        Module AddModule(string name);
        StudentModule GetStudentModule(int id);
        StudentModule AddStudentToModule(int studentId, int moduleId);
        bool RemoveStudentFromModule(int studentId, int moduleId);
        IList<Module> GetAvailableModulesForStudent(int id);
        StudentModule UpdateStudentModuleMark(int studentId, int moduleId, double mark);

        // -------------- Ticket Management -----------------
        Ticket CreateTicket(int studentId, string issue);
        Ticket CloseTicket(int id);
        Ticket GetTicket(int id);
        bool DeleteTicket(int id);

        IList<Ticket> GetAllTickets();
        IList<Ticket> GetOpenTickets();
    }
    
}
