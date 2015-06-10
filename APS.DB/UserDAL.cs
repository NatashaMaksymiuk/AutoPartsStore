using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.DB
{
    public class UserDAL
    {
        public List<User> SelectUsers()
        {
            using (var context = new APSEntities())
            {
                var res = from x in context.Users
                          select x;
                return res.ToList();
            }
        }

        public User GetUser(string login)
        {
            using (var context = new APSEntities())
            {
                return context.Users.FirstOrDefault(u => u.Login == login);
            }
        }

        public void InsertUsers(User us)
        {
            if (us != null)
            {
                if (IsUserValid(us.Login))
                {
                    using (var context = new APSEntities())
                    {
                        context.Users.Add(us);
                        context.SaveChanges();
                    }
                }
            }
        }

        public int GetUserIdByLoggin(String login)
        {
            using (var context = new APSEntities())
            {
                User us = context.Users.FirstOrDefault(u => u.Login == login);
                if (us != null)
                {
                    return us.Id;
                }
            }
            return 0;
        }

        public bool IsUserValid(String login)
        {
            using (var context = new APSEntities())
            {
                User us = context.Users.FirstOrDefault(u => u.Login == login);

                if (us == null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUserLoginValid(string login, string password)
        {
            using (var context = new APSEntities())
            {
                User us = context.Users.FirstOrDefault(u => u.Login == login);
                if (us != null)
                {
                    if (us.Password.Equals(password))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public List<string> GetUserById(int id)
        {
            using (var context = new APSEntities())
            {
                var result = from Us in context.Users
                             where Us.Id == id
                             select Us.Login;

                return result.ToList();
            }
        }

        public string GetUserLoginById(int id)
        {
            using(var context = new APSEntities())
            {
                User us = context.Users.FirstOrDefault(x => x.Id == id);
                return us.Login;
            }
        }
    }
}
