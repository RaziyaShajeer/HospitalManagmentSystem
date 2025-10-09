using HosptitalManagmentSystem.Extenstions;
using HosptitalManagmentSystem.Helpers;
using HosptitalManagmentSystem.MiddleWare;
using HosptitalManagmentSystem.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ? Ensure appsettings.json is included
builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

// ? Add services
builder.Services.AddControllersWithViews();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddTransient<EmailService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// ? Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// ? Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Login"; // ?? login page path
	});

var app = builder.Build();

// ? Disable caching (browser back button prevention)
app.Use(async (context, next) =>
{
	context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
	context.Response.Headers["Pragma"] = "no-cache";
	context.Response.Headers["Expires"] = "0";
	await next();
});

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();           // ? Must come before authentication if you use session data
app.UseAuthentication();    // ? Enable authentication middleware
app.UseAuthorization();     // ? Then authorization

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
