using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ShowTime.Client.Pages;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models.AccommodationInfo;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime.DataAccess.Models.BookingInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using ShowTime.DataAccess.Models.UserInfo;
using ShowTime_BusinessLogic.Abstractions;
using ShowTime_BusinessLogic.Abstractions.Accommodation;
using ShowTime_BusinessLogic.Dtos;
using ShowTime_BusinessLogic.Services;
using ShowTime_BusinessLogic.Services.Accommodations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
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

builder.Services.AddTransient<IRepository<Booking>, GenericRepository<Booking>>();
builder.Services.AddTransient<IBookingService, BookingService>();

builder.Services.AddTransient<IRepository<Accommodation>, GenericRepository<Accommodation>>();
builder.Services.AddTransient<IAccommodationService, AccommodationService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShowTimeDbContext>();
    try
    {
        context.Database.EnsureCreated();
        Console.WriteLine("Database connection successful");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

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
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ShowTime.Client._Imports).Assembly);

app.MapGet("/api/search", async (string q, IArtistService artistService, IFestivalService festivalService) =>
{
    if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
        return Results.Ok(Array.Empty<object>());

    var artists = (await artistService.GetAllAsync())
        .Where(a => a.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
        .Select(a => new {
            Type = "Artist",
            Id = a.Id.ToString(),
            Name = a.Name,
            Image = a.Image
        });

    var festivals = (await festivalService.GetAllAsync())
        .Where(f => f.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
        .Select(f => new {
            Type = "Festival",
            Id = f.Id.ToString(),
            Name = f.Name,
            Image = f.SplashArt
        });

    var results = artists.Concat(festivals).Take(10).ToList();
    return Results.Ok(results);
});

app.Run();
