using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Crossbelt.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var scores = new List<int>() { 20, 40, 60, 80, 100 };

            var ranks = new List<decimal>();
            var maxScore = scores.Max();
            foreach (var score in scores)
            {
                var rank = maxScore/score;
                ranks.Add(rank);
            }
            var result = ranks.Count(x => x <= 3);
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }
            return View();
        }

    }


}