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
            fc["parentTask"] = "1";
             fc["subTaskId"] = "1";
             fc["completed"] = "false";
             fc["parentTask"] = "1";
            tc.UpdateSubtask(fc);
           // string testName = me.name;
           // Assert.AreSame("hi", testName);
           
           

        }
        [Test]
        public void sayHi1()
        {
            Devloper me = new Devloper();
            me.sayNothing();
            string testName = me.name;
            Assert.AreSame("hello", testName);


        }

    }
}