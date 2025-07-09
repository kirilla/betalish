using Betalish.Application.BackgroundServices.Email;
using Betalish.Application.BackgroundServices.Loggers;
using Betalish.Application.BackgroundServices.Reapers;
using Betalish.Application.Queues.BadSignIns;
using Betalish.Application.Queues.EndpointRateLimiting;
using Betalish.Application.Queues.IpAddressRateLimiting;
using Betalish.Application.Queues.LogItems;
using Betalish.Application.Queues.SessionActivities;
using Betalish.Application.Queues.SignInRateLimiting;
using Betalish.Application.Queues.UserEvents;
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

        builder.Services.Configure<SignUpConfiguration>(
            builder.Configuration.GetSection("SignUp"));

        builder.Services.Configure<BadSignInConfiguration>(
            builder.Configuration.GetSection("BadSignIn"));

        builder.Services.Configure<FirewallConfiguration>(
            builder.Configuration.GetSection("Firewall"));

        builder.Services.Configure<SignInConfiguration>(
            builder.Configuration.GetSection("SignIn"));

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

        builder.Services.AddSingleton<IBadSignInList, BadSignInList>();
        builder.Services.AddSingleton<IEndpointRateLimiter, EndpointRateLimiter>();
        builder.Services.AddSingleton<IIpAddressRateLimiter, IpAddressRateLimiter>();
        builder.Services.AddSingleton<ILogItemList, LogItemList>();
        builder.Services.AddSingleton<ISessionActivityList, SessionActivityList>();
        builder.Services.AddSingleton<ISignInRateLimiter, SignInRateLimiter>();
        builder.Services.AddSingleton<IUserEventList, UserEventList>();

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
            builder.Services.AddHostedService<BadSignInLogger>();
            builder.Services.AddHostedService<NetworkRequestReaper>();
            builder.Services.AddHostedService<EmailSender>();
            builder.Services.AddHostedService<EndpointRateLimitReaper>();
            builder.Services.AddHostedService<LogItemLogger>();
            builder.Services.AddHostedService<IpAddressRateLimitReaper>();
            builder.Services.AddHostedService<SessionActivityLogger>();
            builder.Services.AddHostedService<SessionReaper>();
            builder.Services.AddHostedService<SessionActivityReaper>();
            builder.Services.AddHostedService<SignInRateLimitReaper>();
            builder.Services.AddHostedService<SignupReaper>();
            builder.Services.AddHostedService<UserEventLogger>();
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
