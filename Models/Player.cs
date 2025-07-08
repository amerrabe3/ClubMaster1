namespace ClubMaster3.Models
{
    public class Player
    {
  public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int ShirtNum { get; set; }
        public int Age  { get; set; }
        public int TeamId { get; set; }

        public string? ImagePath { get; set; }
    }
}
