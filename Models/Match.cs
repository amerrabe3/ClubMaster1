using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubMaster3.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Display(Name = "Team A")]
        [Required]
        public int TeamAId { get; set; }

        [Display(Name = "Team B")]
        [Required]
        public int TeamBId { get; set; }

        [Display(Name = "Match Date & Time")]
        [Required]
        public DateTime MatchDateTime { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = "Score Team A")]
        public int? ScoreA { get; set; }

        [Display(Name = "Score Team B")]
        public int? ScoreB { get; set; }

        // ✅ Navigation properties — don't mark them [Required]
        [ForeignKey("TeamAId")]
        public Team? TeamA { get; set; }

        [ForeignKey("TeamBId")]
        public Team? TeamB { get; set; }
    }
}
