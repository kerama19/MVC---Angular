using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lab.MemberShip
{
    public class MembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public MembershipService()
            : this(null)
        {
        }

        public MembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public LabMembershipUser ValidateUser(string userName, string password)
        {
            if (_provider.ValidateUser(userName, password))
            {
                return (LabMembershipUser)_provider.GetUser(userName, true);
            }
            return null;
        }
    }
}
