using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurnDown.Models
{


    public interface IChartable
    {

        int id { get; set; }
        System.Nullable<int> developerAvailabilityPerDay { get; set; }
        int originalEstimatedHours { get; set; }
        int hoursSpentOnItem { get; set; }
        int parent { get; set; }
        int priority { get; set; }
        string itemName { get; set; }
        string parentName { get; set; }
        System.Nullable<int> hoursForItemsWithHigherPriority { get; set; }
        DateTime startDate { get; set; }
        DateTime dueDate { get; set; }
        int percentCompleted { get; set; }


    }

    interface IProject : IChartable
    {
        int status { get; set; }
    }

    interface IDeveloper
    {

    }

}