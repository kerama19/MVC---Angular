using Lab.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lab.MemberShip
{
    public class LabPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string function, string role)
        {
            return Identity != null && Identity.IsAuthenticated && Permissions != null && Permissions.Where(x => x.Function == function && x.Rights == role).FirstOrDefault() != null;
           
        }

        public bool IsInRole(string role)
        {
           throw new NotImplementedException();
        }

        public LabPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);            
        }

        public string Email { get; set; }

        public bool Active { get; set; }

        public string CurrentUserName { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
