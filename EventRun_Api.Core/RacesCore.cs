using EventRun_Api.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class RacesCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public RacesCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

        public List<Race> GetRaces(bool checkActive = false)
        {
            try
            {
                return _db.Races.Where(x => x.Active == true || checkActive == false) .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
