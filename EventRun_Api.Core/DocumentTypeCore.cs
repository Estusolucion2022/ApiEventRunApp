using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class DocumentTypeCore
    {
        private readonly EventRunContext _db = new();

        public List<DocumentType> GetDocumentsType() {
            try
            {
                return _db.DocumentTypes.ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
