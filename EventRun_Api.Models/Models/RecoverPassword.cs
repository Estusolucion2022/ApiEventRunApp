using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Models.Models
{
    public class RecoverPassword
    {
        public string UserPlataform { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
