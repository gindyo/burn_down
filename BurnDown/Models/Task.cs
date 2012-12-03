using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurnDown.Models
{
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
    }
}