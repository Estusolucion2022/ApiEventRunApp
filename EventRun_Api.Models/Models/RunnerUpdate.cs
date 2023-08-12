using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Models.Models
{
    public class RunnerUpdate
    {
        public int IdUser { get; set; }
        public string? Description { get; set; }
        public Runner Runner { get; set; } = null!;
    }
}
