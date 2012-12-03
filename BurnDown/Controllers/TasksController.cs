using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BurnDown.Controllers
{
    public class TasksController : Controller
    {
        //
        // GET: /Tasks/

        public ActionResult Index()
        {
            var db = new BurnDown.DB();
            var tasks = db.vTasks;
            return View(tasks);
        }

        //
        // GET: /Tasks/Details/5

        public ActionResult Details(int id)
        {
            var db = new BurnDown.DB();
            var tasks = db.vTasks;

            var task =
                from t in tasks
                where t.taskId == id
                select t;
           
            return View(task.FirstOrDefault());
        }

        //
        // GET: /Tasks/Create

        public ActionResult Create(int id, System.Nullable<int> devId)
        {
            var db = new BurnDown.DB();
            var developers = db.developers;

            IList<SelectListItem> devList = new List<SelectListItem>();

            foreach (BurnDown.Models.developer dev in developers)
            {
                SelectListItem DevItem = new SelectListItem();
                DevItem.Text = dev.firstName + " " + dev.lastName;
                DevItem.Value = dev.developerId.ToString();
                devList.Add(DevItem);
                if (devId == dev.developerId) DevItem.Selected = true; 
                DevItem = null;
            }
            ViewData["ddList"] = devList;
            
              
           
            var projects = db.projects;

            IList<SelectListItem> projList = new List<SelectListItem>();

            foreach (BurnDown.Models.project proj in projects)
            {
                SelectListItem ProjItem = new SelectListItem();
                ProjItem.Text = proj.projectName;
                ProjItem.Value = proj.projectId.ToString();
                projList.Add(ProjItem);
                if (id == proj.projectId) ProjItem.Selected = true;
                ProjItem = null;

            }
            ViewData["projDDList"] = projList;
            return View();
        } 
        

        //
        // POST: /Tasks/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
                // TODO: Add insert logic here


                var newTask = new BurnDown.Models.
                newTask.priority = int.Parse(collection["priority"]);
                newTask.taskName = collection["taskName"];
                newTask.assignedTo = int.Parse(collection["ddList"]);
               // newTask.hoursSpentOnTask = int.Parse(collection["hoursSpentOnTask"]);
                newTask.originalEstimatedHours = int.Parse(collection["originalEstimatedHours"]);
                //newTask.percentCompleted = int.Parse(collection["percentCompleted"]);
                newTask.project = int.Parse(collection["projDDList"]);
             //   newTask.shareOfProject = int.Parse(collection["shareOfProject"]);
                newTask.hoursForTasksWithHigherPriority = int.Parse(collection["hoursForTasksWithHigherPriority"] + "0");
                newTask.startDate = DateTime.Parse(collection["startDate"]);
                newTask.dueDate = DateTime.Parse(collection["dueDate"]);
                var db = new BurnDown.DB();
                var Tasks = db.tasks;
                Tasks.InsertOnSubmit(newTask);
                db.SubmitChanges();
                return RedirectToAction("Index");


            //}
            //catch
            //{
            //    var db = new BurnDown.DB();
            //    var developers = db.developers;

            //    IList<SelectListItem> devList = new List<SelectListItem>();

            //    foreach (BurnDown.Models.developer dev in developers)
            //    {
            //        SelectListItem DevItem = new SelectListItem();
            //        DevItem.Text = dev.firstName + " " + dev.lastName;
            //        DevItem.Value = dev.developerId.ToString();
            //        devList.Add(DevItem);
            //        DevItem = null;
            //    }
            //    ViewData["ddList"] = devList;



            //    var projects = db.projects;

            //    IList<SelectListItem> projList = new List<SelectListItem>();

            //    foreach (BurnDown.Models.project proj in projects)
            //    {
            //        SelectListItem ProjItem = new SelectListItem();
            //        ProjItem.Text = proj.projectName;
            //        ProjItem.Value = proj.projectId.ToString();
            //        projList.Add(ProjItem);
            //        ProjItem = null;
            //    }
            //    ViewData["projDDList"] = projList;
            //    return View();
            //}
        }
        
        //
        // GET: /Tasks/Edit/5
 
        public ActionResult Edit(int id)
        {
            var db = new BurnDown.DB();
            var developers = db.developers;

            IList<SelectListItem> devList = new List<SelectListItem>();

            foreach (BurnDown.Models.developer dev in developers)
            {
                SelectListItem DevItem = new SelectListItem();
                DevItem.Text = dev.firstName + " " + dev.lastName;
                DevItem.Value = dev.developerId.ToString();
                devList.Add(DevItem);
                DevItem = null;
            }
            ViewData["assignedTo"] = devList;



            var projects = db.projects;

            IList<SelectListItem> projList = new List<SelectListItem>();

            foreach (BurnDown.Models.project proj in projects)
            {
                SelectListItem ProjItem = new SelectListItem();
                ProjItem.Text = proj.projectName;
                ProjItem.Value = proj.projectId.ToString();
                projList.Add(ProjItem);
                ProjItem = null;
            }
            ViewData["project"] = projList;

            var tasks = db.tasks;
            var task =
                from t in tasks
                where t.taskId == id
                select t;

            return View(task.FirstOrDefault());

          
        }

        //
        // POST: /Tasks/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            
               
                    var db = new BurnDown.DB();
                    var tasks = db.tasks;
                    var task = tasks
                        .Where(w => w.taskId == id)
                        .SingleOrDefault();
                    task.assignedTo = int.Parse(collection["assignedTo"]);
                    task.dueDate = DateTime.Parse(collection["dueDate"]);
                    task.hoursSpentOnTask= int.Parse(collection["hoursSpentOnTask"]);
                    task.originalEstimatedHours= int.Parse(collection["originalEstimatedHours"]);
                    task.percentCompleted= int.Parse(collection["percentCompleted"]);
                    task.priority = int.Parse(collection["priority"]);
                    task.shareOfProject = int.Parse(collection["shareOfProject"]);
                    task.project = int.Parse(collection["project"]);
                    task.startDate = DateTime.Parse(collection["startDate"]);
                    task.taskName = collection["taskName"];
                    db.SubmitChanges();
                    // TODO: Add insert logic here

                    return RedirectToAction("Details", new { controller = "Projects", id = collection["project"] });
                //    try
                //    { }
                //catch
                //{
                //    var db = new BurnDown.DB();
                //    var developers = db.developers;

                //    IList<SelectListItem> devList = new List<SelectListItem>();

                //    foreach (BurnDown.Models.developer dev in developers)
                //    {
                //        SelectListItem DevItem = new SelectListItem();
                //        DevItem.Text = dev.firstName + " " + dev.lastName;
                //        DevItem.Value = dev.developerId.ToString();
                //        devList.Add(DevItem);
                //        DevItem = null;
                //    }
                //    ViewData["assignedTo"] = devList;



                //    var projects = db.projects;

                //    IList<SelectListItem> projList = new List<SelectListItem>();

                //    foreach (BurnDown.Models.project proj in projects)
                //    {
                //        SelectListItem ProjItem = new SelectListItem();
                //        ProjItem.Text = proj.projectName;
                //        ProjItem.Value = proj.projectId.ToString();
                //        projList.Add(ProjItem);
                //        ProjItem = null;
                //    }
                //    ViewData["project"] = projList;

                //    var tasks = db.tasks;
                //    var task =
                //        from t in tasks
                //        where t.taskId == id
                //        select t;

                //    return View(task.FirstOrDefault());
                //}
            
           
        }

        //
        // GET: /Tasks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tasks/Delete/5

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
        [HttpGet]
        public ActionResult CreateagendaItem(int parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }
        [HttpPost]
        public ActionResult CreateagendaItem(Models.agendaItem newT)
        {
            var db = new BurnDown.DB();
            db.agendaItems.InsertOnSubmit(newT);
            db.SubmitChanges();

            var allPtagendaItems =
                 from aptst in db.agendaItems
                 where aptst.parentTask == newT.parentTask
                 select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.parentTask == newT.parentTask && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == newT.parentTask
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SubmitChanges();

           
            return RedirectToAction("Details", new {id = newT.parentTask});
        }

        [HttpPost]
        public ActionResult CreateagendaItemFromCSL(FormCollection collection)
        {

            
            var db = new BurnDown.DB();
            string[] namesArr = collection["agendaItemName"].Split('\n');
            int parentTask = int.Parse(collection["parentTask"]);

            foreach (string s in namesArr)
            {
                Models.agendaItem newT = new Models.agendaItem();
                newT.agendaItemName = s;
                newT.parentTask = parentTask;
                if (s != "")
                db.agendaItems.InsertOnSubmit(newT);
            }

             
            
            db.SubmitChanges();

            var allPtagendaItems =
                 from aptst in db.agendaItems
                 where aptst.parentTask == parentTask
                 select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.parentTask == parentTask && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == parentTask
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SubmitChanges();


            return RedirectToAction("Details", new { id = parentTask });
        }
        
        public ActionResult GetagendaItems(int id)
        {
            var db = new BurnDown.DB();
            var agendaItems =
                 from st in db.agendaItems
                 where st.parentTask == id
                 select st;
            ViewBag.parentId = id;
            return PartialView(agendaItems);
        }
         
        
        [HttpPost]
        public ActionResult UpdateagendaItem(FormCollection pST)
        {
            var db = new BurnDown.DB();
            
          
            var agendaItem =
                 (from st in db.agendaItems
                 where st.agendaItemId == int.Parse(pST["agendaItemId"])
                 select st).SingleOrDefault();
            agendaItem.agendaItemName = pST["agendaItemName"];
            agendaItem.completed = Boolean.Parse(pST["completed"].Split(new char[] {','})[0]);
            
            db.SubmitChanges();
           
            var allPtagendaItems =
                  from aptst in db.agendaItems
                  where aptst.parentTask == int.Parse(pST["parentTask"])
                  select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.parentTask == int.Parse(pST["parentTask"]) && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b ) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == int.Parse(pST["parentTask"])
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SubmitChanges();
            ViewBag.parentId = int.Parse(pST["parentTask"]);
            return RedirectToAction("Details", new { id = int.Parse(pST["parentTask"]) });
        }

    [HttpGet]
    public ActionResult UpdateagendaItem(int id)
    {
        var db = new BurnDown.DB();

        var agendaItem =
             (from st in db.agendaItems
              where st.agendaItemId == id
              select st).SingleOrDefault();



        return View(agendaItem);
    }
    }
}
