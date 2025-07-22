using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClubMaster3.Data;
using ClubMaster3.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;



namespace ClubMaster3.Controllers
{
    public class AuthController : Controller
    {
        private readonly ClubMaster3Context _context;

        public AuthController(ClubMaster3Context context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "plz fill it";
                return View(model);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View(model);
        }

        // GET: /Auth/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username or email already exists
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            // Hash the password (simple hash for demo; use a stronger hash in production)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = hashedPassword,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["RegisterSuccess"] = "Successfully registered! Please log in.";
            
            
            
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
