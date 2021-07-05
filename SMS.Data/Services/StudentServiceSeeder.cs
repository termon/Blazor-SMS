
using System;
using SMS.Core.Models;

namespace SMS.Data.Services
{
    public static class StudentServiceSeeder
    {
        public static void Seed(IStudentService svc)
        {
            svc.Initialise();

            // Create some students 
            var s1 = svc.AddStudent("Homer", "homer@sms.com", "Computing", 44, "https://avatars2.githubusercontent.com/u/9071?s=400&v=4", 45.0);
            var s2 = svc.AddStudent("Marge", "marge@sms.com", "Engineering", 40, "https://openpsychometrics.org/tests/characters/test-resources/pics/S/3.jpg", 68.0);
            var s3 = svc.AddStudent("Bart",  "bart@sms.com",  "Sleeping",    16, "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/f15f5662080793.5a8432e3d5b6b.jpg", 39.0);
            var s4 = svc.AddStudent("Lisa",  "lisa@sms.com",  "Computing",   13, "https://pyxis.nymag.com/v1/imgs/5e3/936/f401adab450bc7a5efa6740bf4d82af193-09-the-simpsons-apu.rhorizontal.w700.jpg", 86.0);


            // create some modules
            var m1 = svc.AddModule("Programming");
            var m2 = svc.AddModule("Maths");
            var m3 = svc.AddModule("English");
            var m4 = svc.AddModule("French");
            var m5 = svc.AddModule("Physics");
            

            // Add ticket for Homer    
            var t1 = svc.CreateTicket(s1.Id, "Bart you little ...");
            var t11 = svc.CreateTicket(s1.Id, "Which button do I press ...");
            var t12 = svc.CreateTicket(s1.Id, "Mmmmmm Beer ...");
            svc.CloseTicket(t12.Id);

            // Add ticket for Bart
            var t2 = svc.CreateTicket(s3.Id, "Go to Skinners office");

            // Homer is taking programming
            svc.AddStudentToModule(s1.Id, m1.Id);
            svc.AddStudentToModule(s1.Id, m5.Id);
            svc.UpdateStudentModuleMark(s1.Id, m1.Id, 45);
            svc.UpdateStudentModuleMark(s1.Id, m1.Id, 55);

            // Marge is taking maths
            svc.AddStudentToModule(s2.Id, m2.Id);
            svc.AddStudentToModule(s2.Id, m4.Id);
            svc.UpdateStudentModuleMark(s2.Id, m2.Id, 56);
            svc.UpdateStudentModuleMark(s2.Id, m4.Id, 68);

            // Bart is taking English 
            svc.AddStudentToModule(s3.Id, m3.Id);
            svc.AddStudentToModule(s3.Id, m1.Id);
            svc.UpdateStudentModuleMark(s3.Id, m3.Id, 56);
            svc.UpdateStudentModuleMark(s3.Id, m1.Id, 36);

            // Lisa is taking Programming Maths and English
            svc.AddStudentToModule(s4.Id, m1.Id);
            svc.AddStudentToModule(s4.Id, m2.Id);
            svc.AddStudentToModule(s4.Id, m3.Id);
            svc.AddStudentToModule(s4.Id, m4.Id);
            svc.AddStudentToModule(s4.Id, m5.Id);
            svc.UpdateStudentModuleMark(s4.Id, m1.Id, 78);
            svc.UpdateStudentModuleMark(s4.Id, m2.Id, 82);
            svc.UpdateStudentModuleMark(s4.Id, m3.Id, 84);
            svc.UpdateStudentModuleMark(s4.Id, m4.Id, 91);
            svc.UpdateStudentModuleMark(s4.Id, m5.Id, 69);
            
            // add users
            var u1 = svc.RegisterUser("Guest", "guest@sms.com", "guest", Role.Guest);
            var u2 = svc.RegisterUser("Administrator", "admin@sms.com", "admin", Role.Admin);
            var u3 = svc.RegisterUser("Manager", "manager@sms.com", "manager", Role.Manager);

        }


    }


}