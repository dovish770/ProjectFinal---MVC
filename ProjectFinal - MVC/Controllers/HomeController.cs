using Microsoft.AspNetCore.Mvc;
using ProjectFinal___MVC.Models;
using ProjectFinal___MVC.Service;
using System.Diagnostics;

namespace ProjectFinal___MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var agentsSummary = await _httpClient.GetFromJsonAsync<AgentSummery>("http://localhost:5281/agents/summery");//סיכום סוכנים
            var targetsSummary = await _httpClient.GetFromJsonAsync<TargetSummery>("http://localhost:5281/targets/summery");//סיכום מטרות
            var missionsSummary = await _httpClient.GetFromJsonAsync<MissionSummery>("http://localhost:5281/missions/summery");//סיכום משימות

            var Summery = new Summery((agentsSummary.UnderCover+agentsSummary.OnAmission),
                                        targetsSummary.Alive+targetsSummary.Eliminated,
                                        missionsSummary.Actice+missionsSummary.suggestion
                                        +missionsSummary.Complited);

            Summery.GanaralRatio = $"{agentsSummary.OnAmission+agentsSummary.UnderCover}:{targetsSummary.Alive+targetsSummary.Eliminated}";//חישוב של הסוכנים ביחס למטרות
            
            var availableAgents = await _httpClient.GetFromJsonAsync<AvailableAgents>("http://localhost:5281/missions/ratio");//משיכה של מספר סוכנים שניתן לצוותם למשימה

            Summery.ReleventRatio = $"{availableAgents.availableAgents}:{targetsSummary.Alive}";

            return View(Summery);
        }

        public async Task<IActionResult> GetAgents()
        {
            var agents = await _httpClient.GetFromJsonAsync<IEnumerable<Agent>>("http://localhost:5281/Agents");
            var missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5281/Missions");

            AgentWithMission dicAgents = new AgentWithMission();

            foreach (Agent agent in agents)
            {
                var missionByAgentId = missions.FirstOrDefault(miss => miss.Agent.Id == agent.Id);
                if (missionByAgentId != null && missionByAgentId.Status == "Active")
                {                                        
                    dicAgents.agentWithMissions[agent] = $"resquest_for_mission_id={missionByAgentId.Id}";           
                }
                else
                {
                    dicAgents.agentWithMissions[agent] = "";
                }
            }

            return View(dicAgents);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
