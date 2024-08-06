using System.Text;
using HelpMeCook.API.DAO;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Here we will register our dependencies (Services and DbContext, etc) so that we can satisfy our constructors
//and inject dependecies where needed
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
// builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IRecipeRepo, RecipeRepo>();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                 .AddJwtBearer(options => {
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         ValidateIssuer = true,
//                         ValidateAudience = true,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                         ValidAudience = builder.Configuration["Jwt:Audience"],
//                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//                     };
//                 });

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Logging.AddConsole();

builder.Services.AddIdentityApiEndpoints<User>()
        .AddEntityFrameworkStores<AppDbContext>();

//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS
builder.Services.AddCors(co => {
    co.AddPolicy("CORS" , pb =>{
        pb.WithOrigins("*"); //<- your localhost port here!!!
    });
});
//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS        

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();

