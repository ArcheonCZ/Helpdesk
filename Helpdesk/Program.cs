using Helpdesk;
using Helpdesk.Components;
using Helpdesk.Interfaces;
using Helpdesk.Managers;
using Helpdesk.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Razor komponenty (Blazor)
builder.Services.AddRazorPages();      // pro .razor komponenty a layout
builder.Services.AddServerSideBlazor(); // samotný Blazor Server
//builder.Services.AddSignalR(); //->je nutné pøidávat? nemìlo by být

// Registrace EF Core s SQL Serverem
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HelpdeskDbContext>(options =>
	options.UseSqlServer(connectionString));
//Registrace DI
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IIssueManager, IssueManager>();

//Registrace AutoMapper profilu
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


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

// Napojení Blazoru
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
