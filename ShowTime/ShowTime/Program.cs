
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShowTime.Client.Pages;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using ShowTime.DataAccess.Models.UserInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Dtos;
using ShowTime_BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "auth-token";
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    }
    );

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();


var connectionString = builder.Configuration.GetConnectionString("ShowTimeContext");
builder.Services.AddDbContext<ShowTimeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IRepository<Artist>, GenericRepository<Artist>>();
builder.Services.AddTransient<IArtistService, ArtistService>();

builder.Services.AddTransient<IRepository<Festival>, GenericRepository<Festival>>();
builder.Services.AddTransient<IFestivalService, FestivalService>();

builder.Services.AddTransient<IRepository<Lineup>, GenericRepository<Lineup>>();
builder.Services.AddTransient<ILineupService, LineupService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRepository<User>, GenericRepository<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ShowTime.Client._Imports).Assembly);

app.Run();
