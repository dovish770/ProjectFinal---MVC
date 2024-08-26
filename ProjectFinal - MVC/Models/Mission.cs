namespace ProjectFinal___MVC.Models
{
    public class Mission
    {
        public int? Id { get; set; }

        public Agent Agent { get; set; }

        public Target Target { get; set; }

        public double? Duration { get; set; }

        public double TimLeft { get; set; }

        public string Status { get; set; }

        public Mission() { }
    }
}
