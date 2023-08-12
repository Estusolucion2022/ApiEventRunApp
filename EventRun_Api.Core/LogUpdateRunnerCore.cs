using EventRun_Api.Models;
using EventRun_Api.Models.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class LogUpdateRunnerCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public LogUpdateRunnerCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

        public void CreateLog(LogUpdateRunner logUpdateRunner) {
            try
            {
                _db.LogsUpdateRunner.Add(logUpdateRunner);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
