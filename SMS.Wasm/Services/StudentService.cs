using System;
using System.Linq;
using System.Collections.Generic;
using SMS.Core.Dtos;
using SMS.Core.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;


namespace SMS.Wasm.Services
{
    public class StudentService
    {
        private HttpClient client;
        private IConfiguration config;
        private string url;

        public StudentService(HttpClient client,  IConfiguration config)
        {
            this.client = client;
            this.config = config;
            this.url =  config.GetSection("Services")["ApiURL"];
        }

        public async Task<IList<Student>> GetStudents()
        {                  
            var students = await client.GetJsonAsync<IList<Student>>($"{url}/api/student");            
            Console.WriteLine($"GetStudents: {students.Count()}");        
            return students;
        }

        public async Task<StudentDto> AddStudent(StudentDto dto)
        {

            try {
                var student =  await client.PostJsonAsync<StudentDto>($"{url}/api/student", dto);                          
                Console.WriteLine($"AddStudent: {student}");
                return student;    
            } catch {  
                Console.WriteLine($"Error Adding Student");
                return null;
            }
                        
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try {               
                await client.DeleteAsync($"{url}/api/student/{id}");   
                 Console.WriteLine($"Success: DeleteStudent");
                return true;              
            } catch {
                Console.WriteLine($"Error: DeleteStudent");
                return false;
            }
        }

        public async Task<StudentDto> UpdateStudent(StudentDto dto)
        {
            var student =  await client.PutJsonAsync<StudentDto>($"{url}/api/student/{dto.Id}", dto);
            Console.WriteLine($"UpdateStudent: {student}");
            return student;                 
        }
        
        public async Task<StudentDto> GetStudent(int id)
        {
            try {
                var student =  await client.GetJsonAsync<StudentDto>($"{url}/api/student/{id}"); 
                Console.WriteLine($"GetStudent({id}): {student}");
                return student;
            } catch {
                Console.WriteLine($"Error: GetStudent({id})");
                return null;
            }
        }
    }
}