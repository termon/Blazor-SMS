using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using SMS.Core.Dtos;
using SMS.Core.Models;
using SMS.Core.Helpers;

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

        public async Task< IList<StudentDto> > GetStudents()
        {                  
            return await client.GetFromJsonAsync<IList<StudentDto>>($"{url}/api/student");
        }

        public async Task< Union<StudentDto,ProblemDetailsDto> > AddStudent(StudentDto dto)
        {
            var response = await client.PostAsync($"{url}/api/student", JsonContent.Create(dto));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentDto>();
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<ProblemDetailsDto>();
            }           
        }

        public async Task< Union<bool,ProblemDetailsDto> > DeleteStudent(int id)
        {
            var response = await client.DeleteAsync($"{url}/api/student/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<ProblemDetailsDto>();
            } 
        }

        public async Task< Union<StudentDto,ProblemDetailsDto> > UpdateStudent(StudentDto dto)
        {
            var response = await client.PutAsync($"{url}/api/student/{dto.Id}", JsonContent.Create(dto));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentDto>();
            }
            else 
            {
                return await response.Content.ReadFromJsonAsync<ProblemDetailsDto>();
            }                
        }
        
        public async Task< Union<StudentDto,ProblemDetailsDto> > GetStudent(int id)
        {
            var response = await client.GetAsync($"{url}/api/student/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentDto>();
            }
            else 
            {
                return await response.Content.ReadFromJsonAsync<ProblemDetailsDto>();
            } 
        }

        public async Task<bool> VerifyEmailAvailableAsync(string email, int? id)
        {
            return await client.GetFromJsonAsync<bool>($"{url}/api/student/verify/{email}/{id}");
        }
        
    }
}