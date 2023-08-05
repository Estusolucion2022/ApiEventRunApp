using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Models.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        [NotMapped]
        public DateTime CreationDate { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        
        public string UserPlataform { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
