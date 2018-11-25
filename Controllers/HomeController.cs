
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dilo.Models;

namespace Dilo.Controllers
{
    public class HomeController : Controller
    {

        public AppDbContext context;

        public HomeController()
        {
            context = new AppDbContext();
        }
        public IActionResult Index()
        {
             
            ViewBag.ProjectCount = context.Projects.Count();
            ViewBag.FeatureCount = context.WorkItems.Where(w=>w.Category == "Feature").Count();
            ViewBag.TaskCount = context.WorkItems.Where(w=>w.Category == "Task").Count();
            ViewBag.BugCount = context.WorkItems.Where(w=>w.Category == "Bug").Count();
            
            return View();
        }

     
    }
}
