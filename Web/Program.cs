using Application.Services;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Application.DtoValidator;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<ICurrentProjectRepository, CurrentProjectRepository>();
builder.Services.AddScoped<IInternshipDirectionRepository, InternshipDirectionRepository>();
builder.Services.AddScoped<IRepository<Trainee>, TraineeRepository>();
builder.Services.AddScoped<IRepository<CurrentProject>, CurrentProjectRepository>();
builder.Services.AddScoped<IRepository<InternshipDirection>, InternshipDirectionRepository>();
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<TraineeService>();
builder.Services.AddScoped<CurrentProjectService>();
builder.Services.AddScoped<InternshipDirectionsService>();
builder.Services.AddScoped<DeleteResourceService>();
builder.Services.AddScoped<EditResourceNameService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Logging.AddDebug();
builder.Logging.AddConsole();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var user = context.User;
    if (context.Request.Path.StartsWithSegments("/swagger"))
        if (!user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access Denied: Only admins can access Swagger");
            return;
        }

    await next();
});


app.UseStaticFiles();

var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{resourceId?}");


app.MapRazorPages();

app.Run();