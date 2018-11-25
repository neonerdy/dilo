
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Dilo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dilo.Controllers
{
     [Route("[controller]/[action]")]
   
    public class WorkItemController : Controller
    {
        private AppDbContext context;

        public WorkItemController() 
        {
            context = new AppDbContext();
        }
        public async Task<IActionResult> Index()
        {
            var workItems = await context.WorkItems
                .Include(w=>w.Assignee)
                .AsNoTracking()
                .OrderByDescending(w=>w.CreatedDate)
                .ToListAsync();

            return View(workItems);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Category(string name)
        {
            var workItems = await context.WorkItems
                .Include(w=>w.Assignee)
                .AsNoTracking()
                .OrderByDescending(w=>w.CreatedDate)
                .Where(w=>w.Category == name).ToListAsync();
            
            ViewBag.Category = name;

            return View("Index", workItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Project(Guid id)
        {
            var workItems = await context.WorkItems
                .Include(w=>w.Assignee)
                .AsNoTracking()
                .OrderByDescending(w=>w.CreatedDate)
                .Where(w=>w.Project.ID == id).ToListAsync();

            ViewBag.ProjectName = context.Projects.FindAsync(id).Result.ProjectName;

            return View("Index", workItems);
        }

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            string search = Request.Form["txtSearch"];

            var wi = await context.WorkItems
                .Include(w=>w.Project)
                .Include(w=>w.Assignee)
                .Where(w=>w.Tracker.ToUpper() == search.ToUpper())
                .AsNoTracking()
                .SingleOrDefaultAsync();
            
            ViewBag.Search = search;

            if (wi != null) {
                return View("Detail", wi);
            } else {
                var workItems = await context.WorkItems
                    .Include(w=>w.Assignee)
                    .Where(w=>w.Tracker.ToUpper().Contains(search.ToUpper()))
                    .AsNoTracking()
                    .OrderByDescending(w=>w.CreatedDate)
                    .ToListAsync();
                        
                return View("Index", workItems);
            }
        }

     
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(Guid id) 
        {
            var workItem = await context.WorkItems
                .Include(w=>w.Project)
                .Include(w=>w.Assignee)
                .Where(w=>w.ID == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
           
            return View(workItem);
           
        }


        private void AddProjectItem()
        {
            var projects = context.Projects.OrderBy(p=>p.ProjectName);

            List<SelectListItem> projectList = new List<SelectListItem>();
            projectList.Add(new SelectListItem("- Select Project -",""));
            foreach(var p in projects)
            {
                projectList.Add(new SelectListItem(p.ProjectName,p.ID.ToString()));
            }
            ViewBag.Projects = projectList;
        }        

        private void AddAssigneeItem()
        {
            var teams = context.Teams.OrderBy(t=>t.FullName);

            List<SelectListItem> assigneeList = new List<SelectListItem>();
            assigneeList.Add(new SelectListItem("- Select Assignee -",""));
            foreach(var t in teams)
            {
                assigneeList.Add(new SelectListItem(t.FullName,t.ID.ToString()));
            }

            ViewBag.Assignee = assigneeList;
        }


        public IActionResult Add()
        {
            AddProjectItem();
            AddAssigneeItem();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(WorkItem workItem)
        {
            if (ModelState.IsValid)
            {
                workItem.ID = Guid.NewGuid();
                workItem.CreatedDate = DateTime.Now;
                workItem.ModifiedDate = DateTime.Now;
                workItem.Status = "New";

                context.Add(workItem);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else {
                
                 AddProjectItem();
                 AddAssigneeItem();
           
                return View("Add");
            }

            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var workItem = await context.WorkItems.FindAsync(id);
            if (workItem == null) {
                return NotFound();
            }

            AddProjectItem();
            AddAssigneeItem();

            return View(workItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkItem workItem)
        {
            if (ModelState.IsValid)
            {
                workItem.ModifiedDate = DateTime.Now;
                context.Update(workItem);
                await context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            else 
            {
                AddProjectItem();
                AddAssigneeItem();
           
                return View("Add");
            }
            
        }

        [HttpGet("{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(Guid id, string status)
        {
            var workItem = await context.WorkItems.FindAsync(id);
            if (workItem == null)
            {
                return NotFound();
            }
            workItem.Status = status;
            workItem.ModifiedDate = DateTime.Now;
            context.Update(workItem);
            await context.SaveChangesAsync();

            return RedirectToAction("Detail",id);
        }

        [HttpGet("{id}")]      
        public async Task<IActionResult> Delete(Guid id)
        {
            var workItem = await context.WorkItems.FindAsync(id);
            if (workItem == null) {
                return NotFound();
            }
            context.Remove(workItem);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

         
    }
}