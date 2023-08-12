using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Models.Models
{
    public class LogUpdateRunner
    {
        [Key]
        [ForeignKey("IdRunner")]
        public int IdRunner { get; set; }
        [ForeignKey("Iduser")]
        public int Iduser { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public DateTime CreationDate { get; set; }
    }
}
