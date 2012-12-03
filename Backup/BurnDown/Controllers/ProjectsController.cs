using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

namespace BurnDown.Controllers
{
    public class ProjectsController : Controller
    {
        //
        // GET: /Projects/

        public ActionResult Index()
        {

            var db = new BurnDown.DB();
            var projects = db.vProjects;
            //var developers = db.devlopers;
            //Dictionary<int,Models.devloper> devsResultDict = developers.ToDictionary(d=>d.developerId);
            //ViewData["developers"] = devsResultDict;
          
       
           // BurnDown.Models.projectChart pc = new Models.projectChart(projects.ToArray());

           // ViewData["projectChart"] = pc.createProjectsChart("chartCanvas", 150, 400);
            return View(projects);
        }

        //
        // GET: /Projects/Details/5

        public ActionResult Details(int id)
        {
            
            var db = new BurnDown.DB();
            var tasks = db.vTasks;

            var project =
                from t in tasks
                where t.projectId == id
                select t;

            var pr =
                (from t in db.projects
                where t.projectId == id
                select t).Single();
            ViewData["projectName"] = pr.projectName;
            ViewData["projectId"] = pr.projectId;
            ViewData["devId"] = pr.leadDeveloper;
            foreach (var task in project)
            {
                var dev =
                    from t in tasks
                    where t.developerId == task.developerId && t.priority > task.priority
                    select new { t.originalEstimatedHours, t.hoursSpentOnTask };

                int hoursWithHigherPriority = 0;
                foreach (var times in dev)
                {
                    hoursWithHigherPriority = hoursWithHigherPriority + (times.originalEstimatedHours - times.hoursSpentOnTask);
                }

                task.hoursForTasksWithHigherPriority = hoursWithHigherPriority;
                
            }

            Models.projectChart pc = new Models.projectChart(project.ToArray());
            
            ViewData["projectChart"] = pc.createTasksChart("chartCanvas",150, 400);
           
            return View(project);
            
        }
       

        //
        // GET: /Projects/Create

        public ActionResult Create()
        {
            var db = new BurnDown.DB();
            var developers = db.devlopers;
            
            IList<SelectListItem> devList = new List<SelectListItem>();

            foreach (BurnDown.Models.devloper dev in developers)
            {
                SelectListItem DevItem = new SelectListItem();
                DevItem.Text = dev.firstName + " " + dev.lastName;
                DevItem.Value = dev.developerId.ToString();
                devList.Add(DevItem);
                DevItem = null;
            }
            
            ViewData["ddList"] = devList;
            return View();
        } 

        //
        // POST: /Projects/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {

           
                try
                {
                    // TODO: Add insert logic here
                    var newProject = new BurnDown.Models.project();
                    newProject.priority = int.Parse(collection["priority"]);
                    newProject.projectName = collection["projectName"];
                    newProject.leadDeveloper = int.Parse(collection["ddList"]);

                    var db = new BurnDown.DB();
                    var projects = db.projects;
                    projects.InsertOnSubmit(newProject);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            
        }
        
        //
        // GET: /Projects/Edit/5
 
        public ActionResult Edit(int id)
        {
            var db = new BurnDown.DB();
            var project = db.vProjects;
            var proj = from p in project where p.projectId == id select p;
          
            IList<SelectListItem> devList = new List<SelectListItem>();
            var developers = db.devlopers;
            foreach (BurnDown.Models.devloper dev in developers)
            {
                SelectListItem DevItem = new SelectListItem();
                DevItem.Text = dev.firstName + " " + dev.lastName;
                DevItem.Value = dev.developerId.ToString();
                devList.Add(DevItem);
                DevItem = null;
            }

            ViewData["ddList"] = devList;
            return View(proj.FirstOrDefault());
        }

        //
        // POST: /Projects/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var db = new BurnDown.DB();
                var projects = db.projects;

                var newProject =
                    (from p in projects
                     where p.projectId == id
                     select p
                     ).SingleOrDefault();

                newProject.priority = int.Parse(collection["priority"]);
                newProject.projectName = collection["projectName"];
                newProject.leadDeveloper = int.Parse(collection["ddList"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }

        //
        // GET: /Projects/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Projects/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
