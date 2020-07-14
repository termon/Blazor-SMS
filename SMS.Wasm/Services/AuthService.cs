using SMS.Core.Dtos;

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

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

        public async Task<UserDto> RegisterAsync(RegisterDto registerModel)
        {
            // response code must be 20X otherwise PostJsonAsync causes an exception
            try
            {
                return await _httpClient.PostJsonAsync<UserDto>($"{_url}/api/user/register", registerModel);
            } 
            catch (HttpRequestException e)
            {  
                return null;
            }
        }

        public async Task<UserDto> LoginAsync(LoginDto loginModel)
        {
            // response code must be 20X otherwise PostJsonAsync causes an exception
            try {
                var user = await _httpClient.PostJsonAsync<UserDto>($"{_url}/api/user/login", loginModel);      
        
                // add token to localstorage, update auth state with authenticated user, and set default http auth header
                await _localStorage.SetItemAsync("authToken", user.Token);        

                // mark user as logged in and set auth header for future requests      
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.Token);
            
                return user;
            }
            catch (HttpRequestException e)
            {  
                Console.WriteLine("Login Exception " + e);
                return null;
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
