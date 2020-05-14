using SMS.Core.Dtos;

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SMS.Wasm;
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

        public async Task<RegisterResult> RegisterAsync(RegisterRequest registerModel)
        {
            return await _httpClient.PostJsonAsync<RegisterResult>($"{_url}/api/user/register", registerModel);
        }

        public async Task<LoginResult> LoginAsync(LoginRequest loginModel)
        {
            // response code must be 20X otherwise PostJsonAsync causes an exception
            var loginResult = await _httpClient.PostJsonAsync<LoginResult>($"{_url}/api/user/login", loginModel);
           
            if (loginResult.Successful)
            {
                // add token to localstorage, update auth state with authenticated user, and set default http auth header
                await _localStorage.SetItemAsync("authToken", loginResult.Token);                
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            }
            return loginResult;
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
            bool result = await _httpClient.GetJsonAsync<bool>($"{_url}/api/user/verify/{email}");
            Console.WriteLine($"Verifying Email Available: {result}");
            return result;
        }

    }
}
