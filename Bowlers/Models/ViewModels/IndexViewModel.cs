using System;
using System.Collections.Generic;

namespace Bowlers.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Bowlers> Bowlers { get; set; }

        public string TeamName { get; set; }

        public PageNumberingInfo PageNumberingInfo { get; set; }

    }
}
