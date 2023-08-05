using EventRun_Api.Models;
using EventRun_Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class UserCore
    {
        private readonly EventRunContext _db = new();

        public int SaveUser(User user)
        {
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();

                return user.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User? GetUser(string userPlataform, string password)
        {
            try
            {
                User? user = _db.Users.Where(x => x.UserPlataform == userPlataform
                    && x.Password == password).ToList().FirstOrDefault();

                if(user != null) user.Password = "";
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                User user = _db.Users.Where(x => x.Id == id).First();

                user.Password = "";
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
