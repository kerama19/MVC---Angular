using Lab.Data.Model;
using Lab.Data.VirtualModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Business
{    
    public class Permissions
    {
        public static List<Permission> GetPermissions(string username)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var user = context.Users.Where(x => x.UserName == username).FirstOrDefault();

                    if (user != null && user.Permissions != null)
                    {
                        return user.Permissions.OrderBy(x => x.Function).ToList();
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<TempPermissions> GetPermissionsByUser(string username)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var user = context.Users.Include("Permissions").Where(x => x.UserName == username).FirstOrDefault();
                    
                    if (user != null)
                    {
                        var functions = new List<string>() { "Users", "Permissions", "Cars", "Addresses" };

                        List<TempPermissions> tempPermissions = new List<TempPermissions>();

                        foreach(var function in functions)
                        {
                            var permission = new TempPermissions(user, function);
                            tempPermissions.Add(permission);
                        }

                        return tempPermissions;
                    }
                    
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<User> GetUsersWithTempPermissions(string username)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var users = string.IsNullOrEmpty(username) ? 
                        context.Users.Include("Permissions").ToList() : 
                        context.Users.Include("Permissions").Where(x => x.UserName == username).ToList();

                    if (users != null && users.Count > 0)
                    {
                        var functions = new List<string>() { "Users", "Permissions", "Cars", "Addresses" };

                        foreach (var user in users)
                        {  
                            List<TempPermissions> tempPermissions = new List<TempPermissions>();

                            foreach (var function in functions)
                            {
                                var permission = new TempPermissions(user, function);
                                tempPermissions.Add(permission);
                            }

                            user.TempPermissions = tempPermissions;
                            user.Permissions = null;
                        }

                        return users;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<TempPermissions> GetPermissionsList()
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var users = context.Users.Include("Permissions").ToList();

                    if (users != null && users.Count > 0)
                    {
                        var functions = new List<string>() { "Users", "Permissions", "Cars", "Addresses" };

                        List<TempPermissions> tempPermissions = new List<TempPermissions>();

                        foreach (var user in users)
                        { 
                            foreach (var function in functions)
                            {
                                var permission = new TempPermissions(user, function);
                                tempPermissions.Add(permission);
                            }                            
                        }

                        return tempPermissions;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static TempPermissions SavePermissions(TempPermissions permissions)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var permission = context.Permissions.Where(x => x.Function.Equals(permissions.currentFunction) && x.Rights.Equals("Read") && x.UserId.Equals(permissions.UserId)).FirstOrDefault();

                    if(permissions.CanRead)
                    {  
                        if(permission == null)
                        {
                            permission = new Permission();
                            permission.Function = permissions.currentFunction;
                            permission.Rights = "Read";
                            permission.UserId = permissions.UserId;
                            context.Permissions.Add(permission);
                        }                        
                    }
                    else
                    {
                        if (permission != null)
                        {
                            context.Permissions.Remove(permission);
                        } 
                    }

                    permission = context.Permissions.Where(x => x.Function.Equals(permissions.currentFunction) && x.Rights.Equals("Create") && x.UserId.Equals(permissions.UserId)).FirstOrDefault();

                    if (permissions.CanCreate)
                    {
                        if (permission == null)
                        {
                            permission = new Permission();
                            permission.Function = permissions.currentFunction;
                            permission.Rights = "Create";
                            permission.UserId = permissions.UserId;
                            context.Permissions.Add(permission);
                        }
                    }
                    else
                    {
                        if (permission != null)
                        {
                            context.Permissions.Remove(permission);
                        }
                    }

                    permission = context.Permissions.Where(x => x.Function.Equals(permissions.currentFunction) && x.Rights.Equals("Update") && x.UserId.Equals(permissions.UserId)).FirstOrDefault();

                    if (permissions.CanUpdate)
                    {
                        if (permission == null)
                        {
                            permission = new Permission();
                            permission.Function = permissions.currentFunction;
                            permission.Rights = "Update";
                            permission.UserId = permissions.UserId;
                            context.Permissions.Add(permission);
                        }
                    }
                    else
                    {
                        if (permission != null)
                        {
                            context.Permissions.Remove(permission);
                        }
                    }

                    permission = context.Permissions.Where(x => x.Function.Equals(permissions.currentFunction) && x.Rights.Equals("Delete") && x.UserId.Equals(permissions.UserId)).FirstOrDefault();

                    if (permissions.CanDelete)
                    {
                        if (permission == null)
                        {
                            permission = new Permission();
                            permission.Function = permissions.currentFunction;
                            permission.Rights = "Delete";
                            permission.UserId = permissions.UserId;
                            context.Permissions.Add(permission);
                        }
                    }
                    else
                    {
                        if (permission != null)
                        {
                            context.Permissions.Remove(permission);
                        }
                    }

                    context.SaveChanges();

                    var user = context.Users.Include("Permissions").Where(x => x.Id == permissions.UserId).FirstOrDefault();
                    
                    context.Dispose();

                    return new TempPermissions(user, permissions.currentFunction);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static TempPermissions GetUserPermissionsByFucntion(string username, string function)
        {
            using (var context = new MVCLabContext())
            {
                var user = context.Users.Include("Permissions").Where(x => x.UserName == username).FirstOrDefault();

                var tempPermissions = new TempPermissions(user, function);

                if(tempPermissions != null)
                {
                    return tempPermissions;
                }

                return null;
            }
        }
    }
}
