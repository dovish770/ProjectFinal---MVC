namespace ProjectFinal___MVC.Service
{
    public class Suggestion
    {
        public int MissionId { get; set; }

        public string agentName { get; set; }
        public string agentLocation { get; set; }

        public string targetName { get; set; }
        public string targetLocation { get; set; }

        public string distance { get; set; }
        public string timeLeft { get; set; }    
    }
}
