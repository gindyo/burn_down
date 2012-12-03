using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BurnDown.Models
{
    [MetadataType(typeof(BurnDown.ModelsMeta.TaskMeta))]
    public partial class task : IChartable
    {
        public int id
        {
            get
            {
                return this.taskId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }



        int? IChartable.developerAvailabilityPerDay
        {
            get
            {
                return this.developer.hoursPerDayAvailable;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        

        public int hoursSpentOnItem
        {
            get
            {
                return this.hoursSpentOnTask;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int parent
        {
            get
            {
                return this.project_projectId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

       

        public string itemName
        {
            get
            {
                return this.taskName;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string parentName
        {
            get
            {
                return this.project.projectName;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int? hoursForItemsWithHigherPriority
        {
            get
            {
                return this.hoursForTasksWithHigherPriority;
            }
            set
            {
                throw new NotImplementedException();
            }
        }






        int IChartable.percentCompleted
        {
            get
            {
                return Convert.ToInt32(this.percentCompleted);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string status
        {
            get
            {
                
                    string result;
                    double totalHoursEstimated = (double)this.originalEstimatedHours;
                    double hoursWorked = (double)this.hoursSpentOnItem;
                    //  System.Nullable<int> taskDevAvailabilityPerDay = itemToColor.d;


                    double percentageWorkCompleted = Convert.ToInt32(this.percentCompleted);
                    percentageWorkCompleted = percentageWorkCompleted / 100;
                    double percentageOfTimeSpent = hoursWorked / totalHoursEstimated;
                    double hoursRemaining = totalHoursEstimated - hoursWorked;



                    //compare projected date to (current date + 80% of the time remaining to due date)
                    //of it is less keep the color green 
                    // (daysSpent/estimation = percentage in terms of time) compare to workCompletedPercentage 
                    // 

                    string ontimeMin1 = "Early";
                    string ontimeMin2 = "Early";
                    string ontime = "On Time";
                    string ontimePlus1 = "Late";
                    string ontimePlus2 = "Late";

                    double status = (percentageOfTimeSpent / percentageWorkCompleted);

                    if (status > 0.50)
                    {
                        result = ontimeMin2;
                        if (status > 0.90)
                        {
                            result = ontime;
                            if (status > 1.10)
                            {
                                result = ontimePlus1;
                                if (status > 1.50)
                                {
                                    result = ontimePlus2;
                                }
                            }
                        }
                    }
                    else
                        result = ontimeMin1;



                    return result;
                }
            
        }
        
    }
}