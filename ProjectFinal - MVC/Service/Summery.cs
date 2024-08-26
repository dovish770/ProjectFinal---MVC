namespace ProjectFinal___MVC.Service
{
    public class Summery
    {
        public double agents { get; set; }
        public double ActiveAgents { get; set; }
        public double targets { get; set; }
        public double ActiveTargets {  get; set; }
        public double missions { get; set; }
        public double ActiveMissions { get; set; }
        public string GanaralRatio {  get; set; }
        public string ReleventRatio { get; set; }


        public Summery(double agents, double targets, double missions)
        {
            this.agents=agents;
            this.targets=targets;
            this.missions=missions;
        }   
    }
}
