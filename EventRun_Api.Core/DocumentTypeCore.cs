using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class DocumentTypeCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public DocumentTypeCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

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
