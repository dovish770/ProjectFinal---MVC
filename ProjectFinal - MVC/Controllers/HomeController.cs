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

        //מעלה דף בית עם סיכום המצב 
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

            Summery.ReleventRatio = $"{availableAgents.availableAgents}:{targetsSummary.Alive}";//הגדרה של יוחס סוכנים שאפשר לצוותם ביחס למטרות

            Summery.ActiveAgents = agentsSummary.OnAmission;
            Summery.ActiveTargets = targetsSummary.Eliminated;
            Summery.ActiveMissions = missionsSummary.Actice;


            return View(Summery);
        }

        //מעלה דף עם כל הסוכנים ומשימותיהם
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _httpClient.GetFromJsonAsync<IEnumerable<Agent>>("http://localhost:5281/Agents");
            var missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5281/Missions");

            AgentWithMission dicAgents = new AgentWithMission();//מילון שיכיל את הסוכן ואת הריגותיו ומשימתו הפעילה אם יש
            

            foreach (Agent agent in agents)
            {
                MissionSumKills missionSumKills = new MissionSumKills();//מבנה נתונים המכיל משימה פעילה אם יש, וסכום ההריגות
                var missionByAgentId = missions.FirstOrDefault(miss => miss.Agent.Id == agent.Id);//משיכה של כל המשימות של הסוכן
                if (missionByAgentId != null)
                {
                    //מצרף למילון סכום ההריגות עד כה
                    missionSumKills.sumKills = missions.Count(mis => mis.Status == "Comlpeted");

                    //מצרף למילון את המשימה הפעילה אם קיימת
                    if (missionByAgentId.Status == "Active")
                    {
                        missionSumKills.mission = missionByAgentId;
                    }
                }
                dicAgents.agentWithMissions[agent] = missionSumKills;
            }

            return View(dicAgents);
        }


        //מעלה דף של מטרות
        public async Task<IActionResult> GetTargets()
        {
            var tartgets = await _httpClient.GetFromJsonAsync<IEnumerable<Target>>("http://localhost:5281/targets");

            return View(tartgets);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
