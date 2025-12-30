using System.Diagnostics;
using ClubMaster3.Models;
using ClubMaster3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;

namespace ClubMaster3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClubMaster3Context _context;

        public HomeController(ILogger<HomeController> logger, ClubMaster3Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var players = _context.Player.ToList();
            var coaches = _context.Coach.ToList();
            var teams = _context.Team.ToList();

            // إضافة صور افتراضية إذا ما في أي لاعبين
            if (!players.Any())
            {
                players = new List<Player>
                {
                    new Player { Name = "Default Player 1", Position = "Forward", ImagePath = "/images/default-player.jpg" }
                };
            }
            else
            {
                // إذا اللاعب ما عنده صورة → أضف له صورة افتراضية
                foreach (var player in players)
                {
                    if (string.IsNullOrEmpty(player.ImagePath))
                        player.ImagePath = "/images/default-player.jpg";
                }
            }

            // نفس الشيء للمدربين
            if (!coaches.Any())
            {
                coaches = new List<Coach>
                {
                    new Coach { Name = "Default Coach 1",  ImagePath = "/images/default-coach.jpg" }
                };
            }
            else
            {
                foreach (var coach in coaches)
                {
                    if (string.IsNullOrEmpty(coach.ImagePath))
                        coach.ImagePath = "/images/default-coach.jpg";
                }
            }

            // ونفس الشيء للفرق
            if (!teams.Any())
            {
                teams = new List<Team>
                {
                    new Team { Name = "Default Team 1", City = "City A", ImagePath = "/images/default-team.jpg" }
                };
            }
            else
            {
                foreach (var team in teams)
                {
                    if (string.IsNullOrEmpty(team.ImagePath))
                        team.ImagePath = "/images/default-team.jpg";
                }
            }

            var viewModel = new HomeStatsViewModel
            {
                TeamsCount = _context.Team.Count(),
                PlayersCount = _context.Player.Count(),
                CoachesCount = _context.Coach.Count(),
                MatchCount = _context.Matches.Count(),
                Players = players,
                Coaches = coaches,
                Teams = teams
           
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
