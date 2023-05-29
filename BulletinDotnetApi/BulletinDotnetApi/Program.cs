using BulletinDotnetApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IJavaBackendBulletinBoardApiClient, JavaBackendBulletinBoardApiClient>(
    (provider, client) =>
    {
        client.BaseAddress = new Uri(provider.GetService<IConfiguration>()!.GetValue<string>("JavaApiBaseUrl") ?? "ohno");
    });
builder.Services.AddHttpClient<IAzureFunctionApiClient, AzureFunctionApiClient>((provider, client) =>
{
    client.BaseAddress = new Uri(provider.GetService<IConfiguration>()!.GetValue<string>("AzureFunctionApiBasePath")!);
});

// Kun lagt til for debugging av autentisteringsfeil. Viste seg å være en følge av at Zscaler var påslått...
IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-9uq4yxs3.eu.auth0.com/";
    options.Audience = "https://bulletinapi.no.no";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsAllowAny",
        configurePolicy: builder =>
            builder.WithOrigins("http://localhost:5173").AllowCredentials().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseAuthentication(); // lagt til av meg

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsAllowAny");
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();