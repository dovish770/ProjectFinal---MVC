namespace ProjectFinal___MVC.Models
{
    public class Target
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string position { get; set; }

        public int x { get; set; } = new int();

        public int y { get; set; } = new int();

        public string photoUrl { get; set; }

        public string? Status { get; set; }
    }
}
