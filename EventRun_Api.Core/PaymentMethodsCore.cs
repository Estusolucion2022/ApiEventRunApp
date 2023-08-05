using EventRun_Api.Models.Enums;
using EventRun_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Core
{
    public class PaymentMethodsCore
    {
        private readonly EventRunContext _db = new();

        public List<PaymentMethod> GetPaymentMethods()
        {
            try
            {
                return _db.PaymentMethods.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
