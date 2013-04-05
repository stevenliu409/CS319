//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OFRPDMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CenterFreeResource
    {
        public CenterFreeResource()
        {
            this.GivenResources = new HashSet<GivenResource>();
        }
    
        public int Id { get; set; }
        [Required]
        public Nullable<int> NumberAvailable { get; set; }
        public int CenterId { get; set; }
        [Required]
        public string Name { get; set; }
    
        public virtual Center Center { get; set; }
        public virtual ICollection<GivenResource> GivenResources { get; set; }
    }
}
