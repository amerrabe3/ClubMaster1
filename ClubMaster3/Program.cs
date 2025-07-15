using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClubMaster3.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add DbContext
builder.Services.AddDbContext<ClubMaster3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClubMasterConnection")));

// ✅ Add MVC + Session
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Enables session state
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

var app = builder.Build();

// ✅ Configure HTTP Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Enable session BEFORE routing
app.UseSession();

app.UseRouting();
app.UseAuthorization();

// ✅ Role-based / session-based protection middleware
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();

    // Allow login, register, logout, and static files without restriction
    if (path.Contains("/auth/login") || path.Contains("/auth/register") || path.Contains("/auth/logout") ||
        path.Contains("/css") || path.Contains("/js") || path.Contains("/images") ||
        context.Session.GetString("Username") != null)
    {
        await next.Invoke();
    }
    else
    {
        context.Response.Redirect("/Auth/Login");
    }
});

// ✅ Static files and route mapping
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
