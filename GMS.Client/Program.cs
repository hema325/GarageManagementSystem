using Blazored.LocalStorage;
using GMS.Client;
using GMS.Client.Authentication;
using GMS.Client.Interceptors;
using GMS.Client.Services.Account;
using GMS.Client.Services.Brands;
using GMS.Client.Services.Owners;
using GMS.Client.Services.Toastr;
using GMS.Client.Services.Users;
using GMS.Client.Services.Vehicles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// services
builder.Services
    .AddBlazoredLocalStorage();

// dependency injection
builder.Services
    .AddScoped<IToastrService, ToastrService>()
    .AddScoped<IAccountService, AccountService>()
    .AddScoped<IUsersService, UsersService>()
    .AddScoped<IOwnersService, OwnersService>()
    .AddScoped<IBrandsService, BrandsService>()
    .AddScoped<IVehiclesService, VehiclesService>();

// authorization
builder.Services
    .AddAuthorizationCore()
    .AddScoped<CustomeAuthenticationStateProvider>()
    .AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomeAuthenticationStateProvider>())
    .AddScoped<IAuthenticationStateProvider>(sp => sp.GetRequiredService<CustomeAuthenticationStateProvider>());

// http client
builder.Services
    .AddScoped<ErrorHandlerInterceptor>()
    .AddScoped<JwtInterceptor>()
    .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientKeys.APIWithInterceptors));

builder.Services
    .AddHttpClient(HttpClientKeys.APIWithInterceptors, c => c.BaseAddress = new Uri("https://localhost:7218"))
    .AddHttpMessageHandler<ErrorHandlerInterceptor>()
    .AddHttpMessageHandler<JwtInterceptor>();

builder.Services
    .AddHttpClient(HttpClientKeys.APIWithoutInterceptors, c => c.BaseAddress = new Uri("https://localhost:7218"));

await builder.Build().RunAsync();
