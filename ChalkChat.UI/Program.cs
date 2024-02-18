using ChalkChat.App.Managers;
using ChalkChat.Data.Database;
using ChalkChat.Data.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeFolder("/member"));
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeFolder("/Admin", "AdminPolicy"));



builder.Services.AddScoped<IMessageRepo, MessageRepo>();
builder.Services.AddScoped<MessageManager>();
builder.Services.AddScoped<UserManager>();

var connectionStr = builder.Configuration.GetConnectionString("AuthConnection");
var connectionStrTwo = builder.Configuration.GetConnectionString("DbConnection");



builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStr, b => b.MigrationsAssembly("ChalkChat.UI")));
builder.Services.AddDbContext<MessagesDbContext>(options => options.UseSqlServer(connectionStrTwo, b => b.MigrationsAssembly("ChalkChat.UI")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
