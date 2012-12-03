﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using BurnDown.Models;

namespace BurnDown.Tests
{
    [TestFixture]
    public class chartTest
    {
        [Test]
        public void getTasks()
        {
            var db = new BurnDown.Models.DB();
            var tasks = db.tasks;
            
            var project =
                from t in tasks
                where t.project_projectId == 1
                select t;

            var mytask =
               (from t in db.tasks
               where t.project_projectId == 1
               select t).SingleOrDefault();
            //BurnDown.Models.task mytask = new Models.task();
            BurnDown.Models.developer myDev = new Models.developer();

            myDev.hoursPerDayAvailable = 5;
            

         BurnDown.Models.projectChart tc = new BurnDown.Models.projectChart(project.ToArray());



         // Assert.AreEqual("dimitar", tc.generateColor(mytask,3,myDev));

        }
       
       

    }
}