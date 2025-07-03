using ClubMaster3.Models;

namespace ClubMaster3.Models
{
    public class HomeStatsViewModel
    {
        public int TeamsCount { get; set; }
        public int PlayersCount { get; set; }
        public int CoachesCount { get; set; }

        public List<Player> Players { get; set; }
        public List<Coach> Coaches { get; set; }
        public List<Team> Teams { get; set; }
    }
}
