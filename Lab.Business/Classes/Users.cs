using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Data.Model;
using System.Security.Cryptography;

namespace Lab.Business
{
    public class Users 
    {
        public User CurrentUser { get; set; }

        public static string GetMd5Hash(string value)
        {
            var sBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(value))
            {
                var md5Hasher = MD5.Create();
                var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));

                for (var i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        public static void AddUser(User user)
        {
            using (var context = new MVCLabContext())
            {
                context.Users.Add(user);

                context.SaveChanges();

                context.Dispose();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                var pass = GetMd5Hash(password);

                using (var context = new MVCLabContext())
                {
                    var user = context.Users.Where(x => x.UserName == username && x.Password == pass && x.Active).FirstOrDefault();

                    if (user != null)
                    {
                        this.CurrentUser = user;
                        return true;
                    }
                    else
                    {
                        this.CurrentUser = null;
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                this.CurrentUser = null;
                return false;
            }
        }

        public static List<User> GetUserList()
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var users = context.Users.ToList();
                    return users;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static User GetUserByUsername(string username)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var user = (from A in context.Users.ToList()
                                where A.UserName == username
                                select new User { Id = A.Id, Active = A.Active, Email = A.Email, Password = A.Password, UserName = A.UserName }).FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static User SaveUser(User user)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    User currentUser = new User();

                    if (user.Id != 0)
                    {
                        currentUser = context.Users.Include("Permissions").Where(x => x.Id == user.Id).FirstOrDefault();
                    }

                    currentUser.Id = user.Id;
                    currentUser.UserName = user.UserName;
                    currentUser.Password = (string.IsNullOrEmpty(user.Password) ? currentUser.Password : GetMd5Hash(user.Password));
                    currentUser.Email = user.Email;
                    currentUser.Active = user.Active;

                    if (user.Id == 0)
                    {
                        context.Users.Add(currentUser);
                    }

                    context.SaveChanges();

                    context.Dispose();

                    return currentUser;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool DeleteUser(string user)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var currentUser = context.Users.Include("Permissions").Where(x => x.UserName == user).FirstOrDefault();

                    context.Users.Remove(currentUser);

                    context.SaveChanges();

                    context.Dispose();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
