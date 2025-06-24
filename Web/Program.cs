using Betalish.Application.BackgroundServices.Loggers;
using Betalish.Application.BackgroundServices.Reapers;
using Betalish.Application.Queues.LogItems;
using Betalish.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using System.Runtime.Loader;

namespace Betalish.Web;

public class Program
{
    public static void Main(string[] args)
    {
        // App
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        var connectionString = builder.Configuration["Database:ConnectionString"];

        //Console.WriteLine($"ConnectionString: {connectionString}");

        builder.Services.Configure<AccountConfiguration>(
            builder.Configuration.GetSection("Account"));

        builder.Services.Configure<FirewallConfiguration>(
            builder.Configuration.GetSection("Firewall"));

        builder.Services.Configure<SmtpConfiguration>(
            builder.Configuration.GetSection("Smtp"));

        // Dynamic dependency injection
        var files = Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory,
            "Betalish*.dll");

        var assemblies = files
            .Select(p => AssemblyLoadContext.Default.LoadFromAssemblyPath(p));

        builder.Services
            .Scan(p => p.FromAssemblies(assemblies)
            .AddClasses()
            .AsMatchingInterface());

        // Services
        var contentRootPath = $"{builder.Environment.ContentRootPath}\\SessionKeys";

        builder.Services
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(contentRootPath));

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/help/notpermitted";
                options.LoginPath = "/sign-in";
                options.LogoutPath = "/sign-out";
                options.EventsType = typeof(CookieValidator);
            });

        builder.Services.AddSingleton<ILogItemList, LogItemList>();

        builder.Services.AddScoped<CookieValidator>();
        builder.Services.AddScoped<IUserToken, UserToken>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        builder.Services.AddDbContext<DatabaseService>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        if (builder.Environment.IsProduction())
        {
            builder.Services.AddHostedService<LogItemLogger>();
            builder.Services.AddHostedService<BlockedRequestReaper>();
        }

        // API controllers
        builder.Services.AddControllers();

        // Razor
        builder.Services.AddRazorPages();

        // App
        var app = builder.Build();

        // Middleware
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/error");

            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios.
            // https://aka.ms/aspnetcore-hsts.

            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<FirewallMiddleware>();

        app.UseStaticFiles();

        app.UseCookiePolicy(
            new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Lax,
                Secure = CookieSecurePolicy.Always,
            });

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapRazorPages();

        //app.UseEndpoints(endpoints => endpoints.MapControllers());

        //app.MapFallbackToPage("/Payload");

        app.Use(async (context, next) =>
        {
            context.Response.Headers.XFrameOptions = "DENY";
            await next();
        });

        // Run
        app.Run();
    }
}
