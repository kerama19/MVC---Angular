using Lab.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lab.MemberShip
{
    public class LabMembershipUser : MembershipUser
    { 
        public LabMembershipUser()
        {
        }

        public LabMembershipUser(
            string providerName,
	        string name,
	        Object providerUserKey,
	        string email,
	        string passwordQuestion,
	        string comment,
	        bool isApproved,
	        bool isLockedOut,
	        DateTime creationDate,
	        DateTime lastLoginDate,
	        DateTime lastActivityDate,
	        DateTime lastPasswordChangedDate,
	        DateTime lastLockoutDate
            ) :
            base(providerName, name, providerUserKey, email, passwordQuestion,
            comment, isApproved, isLockedOut, creationDate, lastLoginDate,
            lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {

        }

        public string CurrentUserName { get; set; }

        public bool Active { get; set; }

        public List<Permission> Permissions { set; get; }
    }
}
