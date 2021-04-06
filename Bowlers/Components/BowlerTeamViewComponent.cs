using System;
using System.Linq;
using Bowlers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bowlers.Components
{
    public class BowlerTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        // like in the controller, pull the data into a constructor to build the information
        public BowlerTeamViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        // returns default view corresponding to this component
        public IViewComponentResult Invoke()
        {
            return View(context.Teams
                //.Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x));
                //.ToList());
        }
    }
}
