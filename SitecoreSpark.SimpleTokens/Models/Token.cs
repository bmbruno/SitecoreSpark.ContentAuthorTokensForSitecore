using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreSpark.CATS.Models
{
    public class Token
    {
        public Guid ItemID { get; set; }

        public string Pattern { get; set; }

        public string Value { get; set; }
    }
}