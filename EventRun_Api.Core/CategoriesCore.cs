using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class CategoriesCore
    {
        private readonly EventRunContext _db = new();

        public List<Category> GetCategories(bool checkActive = false)
        {
            try
            {
                return _db.Categories.Where(x => x.Active == true || checkActive == false).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
