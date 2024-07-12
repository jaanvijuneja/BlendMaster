using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using WebApplication2.Entities;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TestDb");
builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

// Register OpenAI service
builder.Services.AddSingleton<OpenAIAPI>(sp =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    return new OpenAIAPI(apiKey);
});
builder.Services.AddSingleton<OpenAIService>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
