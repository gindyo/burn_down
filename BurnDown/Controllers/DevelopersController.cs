using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurnDown.Models;

namespace BurnDown.Controllers
{
    public class DevelopersController : Controller
    {
        //
        // GET: /Developers/

        public ActionResult Index()
        {
            var db = new BurnDown.DB();
            var developers = db.developers;
            return View(developers);
        }

        //
        // GET: /Developers/Details/5

        public ActionResult Details(int id)
        {
            var db = new BurnDown.DB();
            var developers = db.developers;
            var dev = from devs in developers where devs.developerId == id select devs;



           var tasks = db.vTasks;

            var devTasks =
                from t in tasks
                where t.assignedTo == id
                select t;

            foreach (var task in devTasks)
            {
                var thp =
                    from t in tasks
                    where t.developerId == task.developerId && t.priority > task.priority
                    select new { t.originalEstimatedHours, t.hoursSpentOnTask };

                int hoursWithHigherPriority = 0;
                foreach (var times in thp)
                {
                    hoursWithHigherPriority = hoursWithHigherPriority + (times.originalEstimatedHours - times.hoursSpentOnTask);
                }

                task.hoursForTasksWithHigherPriority = hoursWithHigherPriority;

            }

            Models.projectChart pc = new Models.projectChart(devTasks.ToArray());

            ViewData["projectChart"] = pc.createTasksChart("chartCanvas", 150, 400);

           // return View(devTasks);





            return View(dev.FirstOrDefault());
        }

        //
        // GET: /Developers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Developers/Create

        [HttpPost]
        public ActionResult Create(BurnDown.Models.developer developer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var db = new BurnDown.DB();
                    db.developers.InsertOnSubmit(developer);
                        db.SubmitChanges();
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(developer);
                }
            }
            else
            {
                return View(developer);
            }
        }
        
        //
        // GET: /Developers/Edit/5
 
        public ActionResult Edit(int id)
        {
            var db = new BurnDown.DB();
            var developers = db.developers;
            var dev = from devs in developers where devs.developerId == id select devs;
            return View(dev.FirstOrDefault());
           
        }

        //
        // POST: /Developers/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BurnDown.Models.developer developer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var db = new BurnDown.DB();
                    var developers = db.developers;
                    var dev = developers
                        .Where(w => w.developerId == developer.developerId)
                        .SingleOrDefault();
                    dev.firstName = developer.firstName;
                    dev.lastName = developer.lastName;
                    dev.email = developer.email;
                    dev.phone = developer.phone;
                    db.SubmitChanges();
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(developer);
                }
            }
            else
            {
                return View(developer);
            }
        }

        //
        // GET: /Developers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Developers/Delete/5

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
