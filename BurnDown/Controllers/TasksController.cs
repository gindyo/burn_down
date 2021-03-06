﻿using System;
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
            var db = new BurnDown.Models.DB();
            var tasks = db.tasks;
            return View(tasks);
        }

        //
        // GET: /Tasks/Details/5

        public ActionResult Details(int id)
        {
            var db = new BurnDown.Models.DB();
            var tasks = db.tasks;

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
            var db = new BurnDown.Models.DB();
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


            var newTask = new BurnDown.Models.task();
                newTask.priority = int.Parse(collection["priority"]);
                newTask.taskName = collection["taskName"];
                newTask.developer_developerId = int.Parse(collection["ddList"]);
               // newTask.hoursSpentOnTask = int.Parse(collection["hoursSpentOnTask"]);
                newTask.originalEstimatedHours = int.Parse(collection["originalEstimatedHours"]);
                //newTask.percentCompleted = int.Parse(collection["percentCompleted"]);
                newTask.project_projectId = int.Parse(collection["projDDList"]);
             //   newTask.shareOfProject = int.Parse(collection["shareOfProject"]);
                newTask.hoursForTasksWithHigherPriority = int.Parse(collection["hoursForTasksWithHigherPriority"] + "0");
                newTask.startDate = DateTime.Parse(collection["startDate"]);
                newTask.dueDate = DateTime.Parse(collection["dueDate"]);
                var db = new BurnDown.Models.DB();
                var Tasks = db.tasks;
                Tasks.AddObject(newTask);
                db.SaveChanges();
                return RedirectToAction("Index");


            //}
            //catch
            //{
            //    var db = new BurnDown.Models.DB();
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
            var db = new BurnDown.Models.DB();
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
            ViewData["developer_developerId"] = devList;



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
            
               
                    var db = new BurnDown.Models.DB();
                    var tasks = db.tasks;
                    var task = tasks
                        .Where(w => w.taskId == id)
                        .SingleOrDefault();
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
                    // TODO: Add insert logic here

                    return RedirectToAction("Details", new { controller = "Projects", id = collection["project"] });
                //    try
                //    { }
                //catch
                //{
                //    var db = new BurnDown.Models.DB();
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
                //    ViewData["developer_developerId"] = devList;



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
            var db = new BurnDown.Models.DB();
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

        [HttpPost]
        public ActionResult CreateagendaItemFromCSL(FormCollection collection)
        {

            
            var db = new BurnDown.Models.DB();
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
         
        
        [HttpPost]
        public ActionResult UpdateAgendaItem(FormCollection pST)
        {
            var db = new BurnDown.Models.DB();
            
          
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

    [HttpGet]
    public ActionResult UpdateAgendaItem(int id)
    {
        var db = new BurnDown.Models.DB();

        var agendaItem =
             (from st in db.agendaItems
              where st.agendaItemId == id
              select st).SingleOrDefault();



        return View(agendaItem);
    }
    }
}
