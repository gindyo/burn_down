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
            updateParentTaskCompletionStatus(newT.task_taskId);
            return RedirectToAction("Details", new {id = newT.task_taskId});
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateagendaItemFromCSL(FormCollection collection)
        {
            string[] namesArr = collection["agendaItemName"].Split('\n');
            int task_taskId = int.Parse(collection["task_taskId"]);

            using (DB db = new DB())
            {
                foreach (string s in namesArr)
                {
                    Models.agendaItem newT = new Models.agendaItem();
                    newT.agendaItemName = s;
                    newT.task_taskId = task_taskId;
                    if (s != "")
                        db.agendaItems.AddObject(newT);
                }

                db.SaveChanges();
                updateParentTaskCompletionStatus(task_taskId);
            }

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
           
            var original_item_id = int.Parse(pST["agendaItemId"]);
           
            agendaItem new_item = new agendaItem();
               new_item.agendaItemName = pST["agendaItemName"];
               new_item.completed = Boolean.Parse(pST["completed"].Split(new char[] { ',' })[0]);
            
            var parent_task_id = int.Parse(pST["task_taskId"]);
            updateAI(original_item_id, new_item);
            updateParentTaskCompletionStatus(parent_task_id);
           
           return RedirectToAction("Details", new { id = parent_task_id });
        }
       
        [Authorize]
        [HttpGet]
        public ActionResult UpdateAgendaItem(int id)
        {
            var agendaItem = db.agendaItems.SingleOrDefault(i => i.agendaItemId == id);
            return View(agendaItem);
        }


        private void updateAI(int original_item_id, agendaItem new_item)
        {
            using (DB db = new DB())
            {
                var original_item = (from oi in db.agendaItems
                                     where oi.agendaItemId == original_item_id
                                     select oi).First();
                original_item.agendaItemName = new_item.agendaItemName;
                original_item.completed = new_item.completed;

                db.SaveChanges();
            }

        }
        private void updateParentTaskCompletionStatus(int parent_task_id){
            using (DB db = new DB())
            {
                var parent_task = (from t in db.tasks
                                  where t.taskId == parent_task_id
                                  select t).SingleOrDefault();
                var totalNumberOfItemsForThisTask = parent_task.agendaItems.Count();
                var numberOfCompletedItems = parent_task.agendaItems.Where(ai => ai.completed == true).Count();
                Decimal taskCompletionPercentage = (decimal)numberOfCompletedItems / (decimal)totalNumberOfItemsForThisTask;
                parent_task.percentCompleted = (int)Math.Round((taskCompletionPercentage * 100), 0);
                db.SaveChanges();
            }
        }




    }
}
