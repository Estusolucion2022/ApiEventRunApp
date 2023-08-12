using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventRun_Api.Core
{
    public class RunnerCore
    {
        private IConfiguration _configuration { get; }
        private readonly EventRunContext _db;
        public RunnerCore(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new(_configuration["AppSettings:DefaultConnection"]!);
        }

        public List<RunnerResponse> GetRunners() {
            try
            {
                List<RunnerResponse> lstRunners = _db.RunnerResponse.FromSqlInterpolated($@"EXEC Get_Runners").ToList();

                return lstRunners;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RunnerResponse? GetRunner(string documentNumber, string documentType) {
            try
            {
                RunnerResponse? runner = _db.RunnerResponse.FromSqlInterpolated($@"EXEC Get_Runners 
                            @DocumentNumber={documentNumber}, @CodeDocumentType={documentType}").ToList().FirstOrDefault();

                return  runner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RunnerResponse GetRunnerById(int id)
        {
            try
            {
                RunnerResponse runner = _db.RunnerResponse.FromSqlInterpolated($@"EXEC Get_Runners 
                            @Id={id}").ToList().First();

                return runner;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SaveRunner(Runner runner) {
            try
            {
                _db.Runners.Add(runner);
                _db.SaveChanges();

                return runner.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateRunner(Runner runner)
        {
            try
            {
                _db.Runners.Update(runner);
                _db.SaveChanges();

                return runner.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
