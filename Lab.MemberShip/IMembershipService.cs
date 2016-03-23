using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lab.MemberShip
{
    public interface IMembershipService
    {
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        LabMembershipUser ValidateUser(string userName, string password);
    }
}
