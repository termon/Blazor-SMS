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
            var response = await client.GetJsonAsync<ApiResponse<IList<Student>>>($"{url}/api/student");
            if (response.IsSuccess) {
                var students = (List<Student>)response.Result;
                Console.WriteLine($"GetStudents: {students.Count()}");        
                return students;
            }
            return new List<Student>();
        }

        public async Task<Student> AddStudent(StudentDto dto)
        {
            try {
                var response =  await client.PostJsonAsync<ApiResponse<Student>>($"{url}/api/student", dto);            
                var student = response.Result;
                Console.WriteLine($"AddStudent: {student}");
                return student;    
            } catch {  
                Console.WriteLine($"Error Adding Student");
                return null;
            }
                        
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var response =  await client.DeleteAsync($"{url}/api/student/{id}"); 
            Console.WriteLine($"DeleteStudent: {response}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Student> UpdateStudent(StudentDto dto)
        {
            var response =  await client.PutJsonAsync<ApiResponse<Student>>($"{url}/api/student/{dto.Id}", dto);
            var student = response.Result; 
            Console.WriteLine($"UpdateStudent: {student}");
            return student;                 
        }
        
        public async Task<StudentDto> GetStudent(int id)
        {
            var response =  await client.GetJsonAsync<ApiResponse<StudentDto>>($"{url}/api/student/{id}");
            var student = response.Result; 
            Console.WriteLine($"GetStudent({id}): {student}");
            return student;  
        }

    }


}