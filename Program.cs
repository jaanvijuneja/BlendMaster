using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BlendMaster.Entities;
using BlendMaster.Services;
using Newtonsoft.Json;
using OpenAI_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register OpenAI service
builder.Services.AddSingleton<OpenAIAPI>(sp =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    return new OpenAIAPI(apiKey);
});
builder.Services.AddSingleton<ChatService>();

// Configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<EnDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlendMasterContext")));
builder.Services.AddScoped<WineService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

// Ensure UseSession is called before UseEndpoints and UseAuthorization
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SelectTable}");

app.MapControllerRoute(
    name: "test",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
