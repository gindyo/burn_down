﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BurnDown.Models
{
    public partial class project : IChartable
    {
        public int id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int? developerAvailabilityPerDay
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int originalEstimatedHours
        {
            get
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                return this.projectName;
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime startDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime dueDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int percentCompleted
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }

         }

        public string Developer_Name
        {
            get{
                return this.developer.full_name;
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


                    double percentageWorkCompleted = this.percentCompleted;
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