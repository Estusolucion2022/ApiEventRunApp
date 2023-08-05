using EventRun_Api.Models.Enums;
using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class CountriesCore
    {
        private readonly EventRunContext _db = new();

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
