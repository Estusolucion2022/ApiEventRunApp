using EventRun_Api.Models;
using EventRun_Api.Models.Enums;
using EventRun_Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class InscriptionDataCore
    {
        private readonly EventRunContext _db = new();

        public List<InscriptionDataResponse> GetInscriptionData(int idRunner)
        {
            try {
                List<InscriptionDataResponse> lstInscriptionData = 
                                        _db.InscriptionDataResponse.FromSqlInterpolated($@"EXEC Get_InscriptionData
                                        @idRunner={idRunner}").ToList();

                return lstInscriptionData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public InscriptionDataResponse? GetInscriptionDataSpecific(int idRunner, int idRace)
        {
            try
            {
                return _db.InscriptionDataResponse.FromSqlInterpolated($@"EXEC Get_InscriptionData @idRunner={idRunner}, @idRace={idRace}").ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ReportInscriptionData> GetReportInscriptionsData()
        {
            try {
                return _db.ReportInscriptionData.FromSqlInterpolated($@"EXEC Get_All_InscriptionData").ToList();
            }
            catch (Exception) { 
            throw;
            }
        }

        public void SaveInscriptionData(InscriptionData inscriptionData) {
            try
            {
                _db.InscriptionData.Add(inscriptionData);
                _db.SaveChanges(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
