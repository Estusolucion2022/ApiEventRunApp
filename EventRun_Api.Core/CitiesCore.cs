using EventRun_Api.Models.Enums;
using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class CitiesCore
    {
        private readonly EventRunContext _db = new();

        public List<City> GetCities()
        {
            try
            {
                return _db.Cities.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
