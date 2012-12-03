using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BurnDown.Models
{
    public partial class developer
    {
        public string full_name
        {
            get
            {
                return new StringBuilder(this.firstName).Append(this.lastName).ToString();
            }
        }
    }
}