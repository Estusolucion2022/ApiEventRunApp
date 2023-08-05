using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class RacesCore
    {
        private readonly EventRunContext _db = new();

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
