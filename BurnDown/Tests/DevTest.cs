using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
namespace BurnDown.Tests
{
    [TestFixture]
    public class DevTest
    {
        [Test]
        public void sayHi()
        {
            BurnDown.Controllers.TasksController tc = new Controllers.TasksController();
            FormCollection fc = new FormCollection();
            fc["task_taskId"] = "1";
             fc["agendaItemId"] = "1";
             fc["completed"] = "false";
             fc["task_taskId"] = "1";
            tc.UpdateAgendaItem(fc);
           // string testName = me.name;
           // Assert.AreSame("hi", testName);
           
           

        }
        [Test]
        public void sayHi1()
        {
            developer me = new developer();
            me.sayNothing();
            string testName = me.name;
            Assert.AreSame("hello", testName);


        }

    }
}