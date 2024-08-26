using ProjectFinal___MVC.Models;

namespace ProjectFinal___MVC.Service
{
    public class AgentWithMission
    {
        public Dictionary<Agent, MissionSumKills> agentWithMissions {  get; set; }

        public AgentWithMission()
        {
            agentWithMissions = new Dictionary<Agent, MissionSumKills>();
        }
    }
}
