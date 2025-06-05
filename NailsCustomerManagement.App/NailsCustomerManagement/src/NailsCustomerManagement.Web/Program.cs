using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Statics;
using NailsCustomerManagement.Application.Services;
using NailsCustomerManagement.Infrastructure.Repositories;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using NLog;
using Microsoft.AspNetCore.Mvc.Razor;
using NailsCustomerManagement.Web;
using NToastNotify;
using Microsoft.AspNetCore.Authentication.Cookies;


var logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure logging
    builder.Logging.ClearProviders();
    builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
    builder.Logging.AddEventSourceLogger();

    //Configure cookies
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.Lax;
    });

    builder.Services.AddAntiforgery(options =>
    {
        //On test and where its not possible to have https 
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });
    builder.Services.AddSession();

    // Add appsettings
    builder.Services.AddOptions();
    builder.Services.Configure<NailsCustomerManagement.Application.AppSettings>(builder.Configuration);
    var settings = builder.Configuration.Get<NailsCustomerManagement.Application.AppSettings>();

    //Add session
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    //Connection to database
    builder.Services.AddDbContext<NailsCustomerManagementDbContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    //Add localization
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    //Configure cultures
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("sr-Cyrl"),
        new CultureInfo("sr-Latn")
    };
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture("en-US"); // default language
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders = new List<IRequestCultureProvider> { new CookieRequestCultureProvider(), new AcceptLanguageHeaderRequestCultureProvider() };
    });

    // Add authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("HomeView", policy => policy.RequireClaim(ClaimTypesExtended.Permission, Permissions.Home.View));
        options.AddPolicy("AppointmentView", policy => policy.RequireClaim(ClaimTypesExtended.Permission, Permissions.Appointment.View));
        options.AddPolicy("SchedulerView", policy => policy.RequireClaim(ClaimTypesExtended.Permission, Permissions.Scheduler.View));

    });

    // Add Cookie Authentication
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/Error/403";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
    
    // Add services to the container.
    builder.Services.AddControllersWithViews()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(SharedResource));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        })
        .AddNToastNotifyToastr(new ToastrOptions()
        {
            ProgressBar = true
        }, new NToastNotifyOption()
        {
            DefaultSuccessTitle = null,
            DefaultWarningTitle = null,
            DefaultErrorTitle = null,
            ScriptSrc = "/lib/toastr/toastr.min.js",
            StyleHref = "/lib/toastr/toastr.min.css"
        });   

    //Dependency Injection
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //Infrastructure
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ILanguageCountryRepository, LanguageCountryRepository>();
    builder.Services.AddScoped<ISessionLogRepository, SessionLogRepository>();
    builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
    builder.Services.AddScoped<IPayementTypeRepository, PayementTypeRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    //Application
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ILanguageCountryService, LanguageCountryService>();
    builder.Services.AddScoped<ISessionLogService, SessionLogService>();
    builder.Services.AddScoped<IPermissionService, PermissionService>();
    builder.Services.AddScoped<IAppointmentService, AppointmentService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    //Security
    app.Use(async (context, next) =>
    {
        // NEW BROWSERS
        var csp = $"object-src 'none'; " +
                  "frame-ancestors 'none';" +
                  "connect-src 'self' http: https: ws: wss:; ";

        context.Response.Headers.Append("Content-Security-Policy", csp);
        // OLD BROWSERS
        context.Response.Headers.Append("X-Frame-Options", "DENY");

        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        await next();
    });

    //Localization middleware
    app.UseRequestLocalization(app.Services
        .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
    app.UseStatusCodePagesWithReExecute("/Error/{0}");

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseNToastNotify();
   
    app.UseSession();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
