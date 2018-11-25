
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
   public class TeamController : Controller
    {
        private AppDbContext context;
        public TeamController() {
            context = new AppDbContext();
        }
        public async Task<IActionResult> Index()
        {
            var teams = await context.Teams
                .OrderBy(t=>t.FullName)
                .ToListAsync();
            
            return View(teams);
        }

        public IActionResult Add() {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var team = await context.Teams.FindAsync(id);
            if (team == null) {
                return NotFound();
            }
          
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Team team)
        {
            
           if (ModelState.IsValid)
           {
                team.ID = Guid.NewGuid();
                context.Add(team);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            } 
            else {
                return View("Add");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            if (ModelState.IsValid)
            {
                context.Update(team);
                await context.SaveChangesAsync();
            }
            else{
                return View("Edit");
            }
            return RedirectToAction("Index");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var team = await context.Teams.FindAsync(id);
            if (team == null) {
                return NotFound();
            }
            context.Remove(team);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}