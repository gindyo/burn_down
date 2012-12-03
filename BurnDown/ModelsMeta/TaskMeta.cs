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
        public int percentCompleted;
    }
}