using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurnDown.Models;
using System.Data.Objects;

namespace BurnDown.Controllers
{
    public class TasksController : Controller
    {
        //
        // GET: /Tasks/
        private DB db{ get{return new DB();}}

        private ObjectSet<task> tasks{ get { return db.tasks; }}

        private task task { 
            get {
                int id = Convert.ToInt32(this.RouteData.Values["id"]);
                return db.tasks.SingleOrDefault(t => t.taskId == id);
            }
        }

        private IList<SelectListItem> devList
        {
            get{
             IList<SelectListItem> devList = new List<SelectListItem>();
             
             foreach (BurnDown.Models.developer dev in db.developers)
             {
                 SelectListItem DevItem = new SelectListItem();
                 DevItem.Text = dev.firstName + " " + dev.lastName;
                 DevItem.Value = dev.developerId.ToString();
                 devList.Add(DevItem);
                 try
                 {
                     if (task.developer.developerId == dev.developerId) DevItem.Selected = true;
                 }
                 catch { }
                 DevItem = null;
             }
             return devList;
            }
        }

        private IList<SelectListItem> projList{
            get{
                IList<SelectListItem> projList = new List<SelectListItem>();

                foreach (BurnDown.Models.project proj in db.projects)
                {
                    SelectListItem ProjItem = new SelectListItem();
                    ProjItem.Text = proj.projectName;
                    ProjItem.Value = proj.projectId.ToString();
                    projList.Add(ProjItem);
                    try
                    {
                        if (task.project.projectId == proj.projectId) ProjItem.Selected = true;
                    }
                    catch{}
                    ProjItem = null;

                }
                return projList;
            }
        }


        public ActionResult Index()
        {
            return View(tasks);
        }

        //
        // GET: /Tasks/Details/5

        public ActionResult Details(int id)
        {
            return View(tasks.FirstOrDefault(t=>t.taskId==id));
        }


        //
        // GET: /Tasks/Create

        [Authorize]
        public ActionResult Create(int project_id, System.Nullable<int> devId)
        {
            
            var developers = db.developers;
            ViewData["ddList"] = devList;
            ViewData["projDDList"] = projList;
            return View();
        } 
        

        //
        // POST: /Tasks/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
               var newTask = new BurnDown.Models.task();
                newTask.priority = int.Parse(collection["priority"]);
                newTask.taskName = collection["taskName"];
                newTask.developer_developerId = int.Parse(collection["ddList"]);
                newTask.originalEstimatedHours = int.Parse(collection["originalEstimatedHours"]);
                newTask.project_projectId = int.Parse(collection["projDDList"]);
                newTask.hoursForTasksWithHigherPriority = int.Parse(collection["hoursForTasksWithHigherPriority"] + "0");
                newTask.startDate = DateTime.Parse(collection["startDate"]);
                newTask.dueDate = DateTime.Parse(collection["dueDate"]);
                
                tasks.AddObject(newTask);
                db.SaveChanges();
                
                return RedirectToAction("Index");

        }
        
        //
        // GET: /Tasks/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewData["developer_developerId"] = devList;
            ViewData["project"] = projList;
            return View(task);

        }

        //
        // POST: /Tasks/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {          
                    task.developer_developerId = int.Parse(collection["developer_developerId"]);
                    task.dueDate = DateTime.Parse(collection["dueDate"]);
                    task.hoursSpentOnTask= int.Parse(collection["hoursSpentOnTask"]);
                    task.originalEstimatedHours= int.Parse(collection["originalEstimatedHours"]);
                    task.percentCompleted= int.Parse(collection["percentCompleted"]);
                    task.priority = int.Parse(collection["priority"]);
                    task.shareOfProject = int.Parse(collection["shareOfProject"]);
                    task.project_projectId = int.Parse(collection["project"]);
                    task.startDate = DateTime.Parse(collection["startDate"]);
                    task.taskName = collection["taskName"];
                    db.SaveChanges();
                    return RedirectToAction("Details", new { controller = "Projects", id = collection["project"] });
              
        }

        //
        // GET: /Tasks/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(task);
        }

        //
        // POST: /Tasks/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DB dd = new DB();
                var task = dd.tasks.SingleOrDefault(t => t.taskId == id);
                dd.DeleteObject(task);
                dd.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.e = e;
                return View(task);
            }
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult CreateagendaItem(int parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult CreateagendaItem(Models.agendaItem newT)
        {
            db.agendaItems.AddObject(newT);
            db.SaveChanges();

            var allPtagendaItems =
                 from aptst in db.agendaItems
                 where aptst.task_taskId == newT.task_taskId
                 select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.task_taskId == newT.task_taskId && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == newT.task_taskId
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SaveChanges();

           
            return RedirectToAction("Details", new {id = newT.task_taskId});
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateagendaItemFromCSL(FormCollection collection)
        {
            string[] namesArr = collection["agendaItemName"].Split('\n');
            int task_taskId = int.Parse(collection["task_taskId"]);

            foreach (string s in namesArr)
            {
                Models.agendaItem newT = new Models.agendaItem();
                newT.agendaItemName = s;
                newT.task_taskId = task_taskId;
                if (s != "")
                db.agendaItems.AddObject(newT);
            }

            db.SaveChanges();

            var allPtagendaItems =
                 from aptst in db.agendaItems
                 where aptst.task_taskId == task_taskId
                 select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.task_taskId == task_taskId && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == task_taskId
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SaveChanges();


            return RedirectToAction("Details", new { id = task_taskId });
        }
        
        public ActionResult GetagendaItems(int id)
        {
            var db = new BurnDown.Models.DB();
            var agendaItems =
                 from st in db.agendaItems
                 where st.task_taskId == id
                 select st;
            ViewBag.parentId = id;
            return PartialView(agendaItems);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateAgendaItem(FormCollection pST)
        {
           
            var agendaItem =
                 (from st in db.agendaItems
                 where st.agendaItemId == int.Parse(pST["agendaItemId"])
                 select st).SingleOrDefault();
            agendaItem.agendaItemName = pST["agendaItemName"];
            agendaItem.completed = Boolean.Parse(pST["completed"].Split(new char[] {','})[0]);
            
            db.SaveChanges();
           
            var allPtagendaItems =
                  from aptst in db.agendaItems
                  where aptst.task_taskId == int.Parse(pST["task_taskId"])
                  select aptst;

            var completedPtagendaItems =
                    from aptst in db.agendaItems
                    where aptst.task_taskId == int.Parse(pST["task_taskId"]) && aptst.completed == true
                    select aptst;


            double a = completedPtagendaItems.Count();
            double b = allPtagendaItems.Count();


            double percentageCompleted = (double)(a / b ) * 100;

            BurnDown.Models.task paretnTask =
                (from pt in db.tasks
                 where pt.taskId == int.Parse(pST["task_taskId"])
                 select pt).SingleOrDefault();

            paretnTask.percentCompleted = (int)Math.Round(percentageCompleted, 0);
            db.SaveChanges();
            ViewBag.parentId = int.Parse(pST["task_taskId"]);
            return RedirectToAction("Details", new { id = int.Parse(pST["task_taskId"]) });
        }
       
        [Authorize]
        [HttpGet]
        public ActionResult UpdateAgendaItem(int id)
    {
       

        var agendaItem =
             (from st in db.agendaItems
              where st.agendaItemId == id
              select st).SingleOrDefault();



        return View(agendaItem);
    }
    }
}
