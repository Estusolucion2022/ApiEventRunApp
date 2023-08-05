using EventRun_Api.Models.Enums;
using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventRun_Api.Core
{
    public class CountriesCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public CountriesCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

        public List<Country> GetCountries()
        {
            try
            {
                return _db.Countries.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
