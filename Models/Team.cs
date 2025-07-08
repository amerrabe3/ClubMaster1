using System.ComponentModel.DataAnnotations.Schema;

namespace ClubMaster3.Models
{
    [Table("Teams")]
    public class Team
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string City { get; set; }  
       
        public int FoundedYear { get; set; } 

        public int CoachId { get; set; }

        public string? ImagePath { get; set; }
    }
}

