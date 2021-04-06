using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bowlers.Models;
using Bowlers.Models.ViewModels;

namespace Bowlers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }




        public IActionResult Index(long? teamId, int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowlers = context.Bowlers
                    .Where(b => b.TeamId == teamId || teamId == null)
                    .OrderBy(b => b.TeamId).ThenBy(b => b.BowlerLastName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),

                TeamName =
                    (teamId == null ? null :
                    context.Teams
                        .Where(t => t.TeamId == teamId)
                        .Select(t => t.TeamName)
                        .FirstOrDefault()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    // If no team has been selected, get the full count. Else, only
                    // count those that match the selected team (passed in from the view)
                    TotalNumItems =
                        (teamId == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamId).Count())
                }
            });


        }

            

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
