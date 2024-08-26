using System.ComponentModel.DataAnnotations;

namespace ProjectFinal___MVC.Models
{
    public class Agent
    {        
        public int? Id { get; set; }

        public string nickname { get; set; }

        public string? Status { get; set; }

        public int x { get; set; } = 0;

        public int y { get; set; } = 0;

        public string photoUrl { get; set; }
    }
}
