//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App3
{
    using System;
    using System.Collections.Generic;
    
       public class Driver
        { 
        public Driver()
        {
            this.Missions = new HashSet<Mission>();
        }
    
        public int id_driver { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Adress { get; set; }
        public string password { get; set; }
        public string username { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
