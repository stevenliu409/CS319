using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFRPDMS.Models
{
    public class ChildAndGuardian
    {
        public PrimaryGuardian PrimaryGuardian{get;set;}
        public Child Child { get; set; }
    }
}