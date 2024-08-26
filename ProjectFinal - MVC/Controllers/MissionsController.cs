using Microsoft.AspNetCore.Mvc;
using ProjectFinal___MVC.Models;
using ProjectFinal___MVC.Service;
using System.Net.Http;

namespace ProjectFinal___MVC.Controllers
{
    public class MissionsController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<MissionsController> _logger;

        public MissionsController(ILogger<MissionsController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetMissions()
        {
            var missions = await _httpClient.GetFromJsonAsync<IEnumerable<Mission>>("http://localhost:5281/Missions");

            var MissionSuggestens = missions.Where(mis => mis.Status == "Suggestion");

            var suggestions = new List<Suggestion>();

            foreach (var mission in MissionSuggestens)
            {
                var suggestion = new Suggestion();

                
                suggestion.agentName = mission.Agent.nickname;
                suggestion.agentLocation = $"({mission.Agent.x}, {mission.Agent.y})";
                
                suggestion.agentName = mission.Target.name;
                suggestion.agentLocation = $"({mission.Target.x}, {mission.Target.y})";

                suggestion.timeLeft = mission.TimLeft.ToString();

                suggestion.distance = Math.Sqrt(Math.Pow(mission.Agent.x - mission.Target.x, 2) + Math.Pow(mission.Agent.y - mission.Target.y, 2)).ToString();

                suggestion.MissionId = mission.Id;

                suggestions.Add(suggestion);
            }

            return View(suggestions);
        }


        public async Task<IActionResult> StartMission(int id)
        {                                      
            var response = await _httpClient.PutAsync($"http://localhost:5281/Missions/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Mission has started!";
            }
            else
            {                    
                TempData["Message"] = "cannot start mission that is fardest then 200 km!";               
            }
           
            return RedirectToAction("GetMissions");
        }



    }
}
