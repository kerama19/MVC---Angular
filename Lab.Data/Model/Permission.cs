//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {
        public int Id { get; set; }
        public string Function { get; set; }
        public string Rights { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
    }
}