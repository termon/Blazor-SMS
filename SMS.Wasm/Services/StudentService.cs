using System;
using System.Linq;
using System.Collections.Generic;
using SMS.Core.Dtos;
using SMS.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

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
            var students = await client.GetJsonAsync<Student[]>($"{url}/api/student");
            Console.WriteLine($"GetStudents: {students.Count()}");
            return students;
        }

        public async Task<Student> AddStudent(StudentDto dto)
        {
            var student =  await client.PostJsonAsync<Student>($"{url}/api/student", dto); 
            Console.WriteLine($"AddStudent: {student}");
            return student;                 
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var response =  await client.DeleteAsync($"{url}/api/student/{id}"); 
            Console.WriteLine($"DeleteStudent: {response}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Student> UpdateStudent(StudentDto dto)
        {
            var student =  await client.PutJsonAsync<Student>($"{url}/api/student/{dto.Id}", dto); 
            Console.WriteLine($"UpdateStudent: {student}");
            return student;                 
        }
        
        public async Task<StudentDto> GetStudent(int id)
        {
            var student =  await client.GetJsonAsync<StudentDto>($"{url}/api/student/{id}");
            Console.WriteLine($"GetStudent({id}): {student}");
            return student;  
        }

    }


}