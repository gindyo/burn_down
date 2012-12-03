using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BurnDown.ModelsMeta
{
    public class TaskMeta
    {
        [Validations.PercentCompletedValidation]
        [Display(Name="Percent Completed")]
        public int percentCompleted;

        [Display(Name = "Original Estimated Hours")]
        public int originalEstimatedHours;

        [Display(Name = "Hours Spent On Task")]
        public int hoursSpentOnTask;

        [Display(Name = "Priority")]
        public int priority;

        [Display(Name = "Task Name")]
        public string taskName;

        [Display(Name = "Due Date")]
        public DateTime dueDate;


    }
}