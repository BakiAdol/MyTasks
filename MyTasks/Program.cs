using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using MyTasksClassLib.Models;
using MyTasks.Helpers.ClaimsHelper;
using MyTasks.Services.IServices;
using MyTasks.Services;

var builder = WebApplication.CreateBuilder(args);

// dependencie injection
builder.Services.AddScoped<IMyTasksService, MyTasksService>();
builder.Services.AddScoped<IMyTaskRepository, MyTaskRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// db connection build
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserModel, IdentityRole>().
    AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt => {
    opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Admin/AccessDenied");
});

builder.Services.AddScoped<IUserClaimsPrincipalFactory<UserModel>,
    ApplicationUserClaimsPrincipalFactory>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
