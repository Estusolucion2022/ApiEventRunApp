﻿using EventRun_Api.Models;
using EventRun_Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class UserCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public UserCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

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

        public string? GetEmailByUser(string user)
        {
            try
            {
                User users = _db.Users.Where(x => x.UserPlataform == user).First();
                if (users != null) return users.Email;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePasswordUser(RecoverPassword model)
        {
            try
            {
                User user = _db.Users.Where(x => x.UserPlataform == model.UserPlataform).First();
                if (user != null)
                {
                    user.Password = model.Password;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;    
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
