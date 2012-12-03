using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BurnDown.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PercentCompletedValidation : ValidationAttribute
    {
        private int max = 100;

        public PercentCompletedValidation() : base("Percentage completed can not be more then 100%")
        { }
        public override bool IsValid(object value)
        {
            decimal pc = (decimal)value;
            if (this.max < pc)
                return false;
            return true;
        }
        
    }
}