
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Dilo.Models;

namespace Dilo.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {

        private AppDbContext context;
        public ProjectController()
        {
            context = new AppDbContext();
        }

        public async Task<IActionResult> Index()
        {
            var projects = await context.Projects
                .OrderByDescending(p=>p.CreatedDate)
                .ToListAsync();

            return View(projects);
        }

        public IActionResult Add()
        {
            return View();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Project project)
        {
            if (ModelState.IsValid)
            {
                project.ID = Guid.NewGuid();
                project.CreatedDate = DateTime.Now;
                project.Status = "New";
                context.Add(project);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            } 
            else {
                return View("Add");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Project project)
        {
            if (ModelState.IsValid)
            {
                context.Update(project);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            } 
            else {
                return View("Edit");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null) {
                return NotFound();
            }
            context.Remove(project);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
    }
}