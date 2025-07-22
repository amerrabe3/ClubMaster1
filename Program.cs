using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClubMaster3.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add DbContext
builder.Services.AddDbContext<ClubMaster3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClubMasterConnection")));

// ✅ Add MVC + Session + Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// ✅ Add Authorization (for [Authorize] attributes)
builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ HTTP Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

// ✅ Enable JWT authentication for APIs
app.UseAuthentication();
app.UseAuthorization();

// ✅ Protect MVC pages using Session (skip APIs)
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();

    if ((path != null && path.StartsWith("/api")) ||
        (path != null && (path.Contains("/auth/login") || path.Contains("/auth/register") || path.Contains("/auth/logout") ||
                          path.Contains("/css") || path.Contains("/js") || path.Contains("/images"))) ||
        context.Session.GetString("Username") != null)
    {
        await next.Invoke();
    }
    else
    {
        context.Response.Redirect("/Auth/Login");
    }
});

// ✅ Map default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
