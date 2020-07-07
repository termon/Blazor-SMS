
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
            var s1 = svc.AddStudent("Homer", "homer@mail.com", "Computing", 44, "https://avatars2.githubusercontent.com/u/9071?s=400&v=4", 45.0);
            var s2 = svc.AddStudent("Marge", "marge@mail.com", "Engineering", 40, "https://openpsychometrics.org/tests/characters/test-resources/pics/S/3.jpg", 68.0);
            var s3 = svc.AddStudent("Bart",  "bart@mail.com",  "Sleeping",    16, "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/f15f5662080793.5a8432e3d5b6b.jpg", 39.0);
            var s4 = svc.AddStudent("Lisa",  "lisa@mail.com",  "Computing",   13, "https://pyxis.nymag.com/v1/imgs/5e3/936/f401adab450bc7a5efa6740bf4d82af193-09-the-simpsons-apu.rhorizontal.w700.jpg", 86.0);


            // create some modules
            var m1 = svc.AddModule("Programming");
            var m2 = svc.AddModule("Maths");
            var m3 = svc.AddModule("English");

            // Add ticket for Homer    
            var t1 = svc.CreateTicket(s1.Id, "Bart you little ...");
            var t11 = svc.CreateTicket(s1.Id, "Which button do I press ...");

            // Add ticket for Bart
            var t2 = svc.CreateTicket(s3.Id, "Go to Skinners office");

            // Homer is taking programming
            svc.AddStudentToModule(s1.Id, m1.Id);

            // Marge is taking maths
            svc.AddStudentToModule(s2.Id, m2.Id);

            // Bart is taking English 
            svc.AddStudentToModule(s3.Id, m3.Id);

            // Lisa is taking Programming Maths and English
            svc.AddStudentToModule(s4.Id, m1.Id);
            svc.AddStudentToModule(s4.Id, m2.Id);
            svc.AddStudentToModule(s4.Id, m3.Id);

            // add users
            var u1 = svc.RegisterUser("Guest", "guest@sms.com", "guest", Role.Guest);
            var u2 = svc.RegisterUser("Administrator", "admin@sms.com", "admin", Role.Admin);
            var u3 = svc.RegisterUser("Manager", "manager@sms.com", "manager", Role.Manager);

        }


    }


}