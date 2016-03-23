using Lab.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Data.VirtualModel
{
    public class TempPermissions
    {
        [Display(Name = "Can Read?")]
        public bool CanRead { get; set; }
        
        [Display(Name = "Can Create?")]
        public bool CanCreate { get; set; }
        
        [Display(Name = "Can Update?")]
        public bool CanUpdate { get; set; }
        
        [Display(Name = "Can Delete?")]
        public bool CanDelete { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string currentFunction { get; set; }

        public TempPermissions(User user, string function)
        {
            var permissions = new List<Permission>();

            if (user.Permissions != null && user.Permissions.Count > 0)
            {
                permissions = user.Permissions.Where(x => x.Function.Equals(function)).ToList();
            }

            this.UserId = user.Id;
            this.currentFunction = function;
            this.UserName = user.UserName;

            this.CanRead = permissions.ToList().Exists(x => x.Rights.Equals("Read"));
            this.CanCreate = permissions.ToList().Exists(x => x.Rights.Equals("Create"));
            this.CanUpdate = permissions.ToList().Exists(x => x.Rights.Equals("Update"));
            this.CanDelete = permissions.ToList().Exists(x => x.Rights.Equals("Delete"));
        }

        public TempPermissions()
        {
        }
    }
}
