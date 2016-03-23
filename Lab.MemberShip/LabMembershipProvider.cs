using Lab.Business;
using Lab.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lab.MemberShip
{
    public class LabMembershipProvider : MembershipProvider
    {
        private LabMembershipUser _user;
        
        #region "Not Implemented Membership Provider Elements

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }        

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser User)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region "Implmented Membership Provider Elements"

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _user != null && _user.UserName == username ? _user : null;
            return user;
        }        
        
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if(args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if(RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var user = GetUser(username, true);

            if(user == null)
            {
                var newUser = new User { UserName = username, Password = Users.GetMd5Hash(password) };
                Users.AddUser(newUser);

                status = MembershipCreateStatus.Success;

                return GetUser(username, true);
            }

            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool ValidateUser(string username, string password)
        {
            var users = new Users();

            var result = users.ValidateUser(username, password);

            if (users.CurrentUser != null)
            {
                _user = new LabMembershipUser("LabMembershipProvider", username, string.Empty, (users.CurrentUser != null ? users.CurrentUser.Email : string.Empty),
                                                string.Empty, string.Empty, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.Now);

                _user.Active = users.CurrentUser.Active;
                _user.CurrentUserName = users.CurrentUser.UserName;                
            }
            return result;

        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }        

        #endregion
    }
}
