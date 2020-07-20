using SMS.Core.Dtos;

using System;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Blazored.LocalStorage;
using SMS.Core.Helpers;

namespace SMS.Wasm.Services
{
    public class AuthService 
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private readonly string _url;
        
        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           IConfiguration config)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _config = config;
            _url = config.GetSection("Services")["ApiURL"];
        }

        public async Task< Union<UserDto,ErrorResponse> > RegisterAsync(RegisterDto registerModel)
        {
            // register user and check response
            var response = await _httpClient.PostAsync($"{_url}/api/user/register", JsonContent.Create(registerModel));      
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();              
                return error;
            }
        }

       public async Task< Union<UserDto,ErrorResponse> > LoginAsync(LoginDto loginModel)
        {
            // submit login request and check response
            var response = await _httpClient.PostAsync($"{_url}/api/user/login", JsonContent.Create(loginModel));
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>(); 
                // add token to localstorage, update auth state with authenticated user, and set default http auth header
                await _localStorage.SetItemAsync("authToken", user.Token);        

                // mark user as logged in and set auth header for future requests      
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.Token);
                return user;
            }
            else 
            {
                // webapi unsuccessful status codes return an ErrorResponse object
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>(); 
                return error;
            }          
  
        }

        public async Task LogoutAsync()
        {
            // remove token from local storage
            await _localStorage.RemoveItemAsync("authToken");
            // update auth state with anonymous user
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            // remove http client Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> VerifyEmailAvailableAsync(string email)
        {
            return await _httpClient.GetJsonAsync<bool>($"{_url}/api/user/verify/{email}");
        }

    }
}
