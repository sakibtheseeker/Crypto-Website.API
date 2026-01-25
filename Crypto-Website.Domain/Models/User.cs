using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

        public class User
        {
            [Key]
            public int Uid { get; set; }

            public string Uname { get; set; }
            public string Uemail { get; set; }
            public string Upassword { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public int CreatedBy { get; set; }

            public DateTime? UpdatedAt { get; set; }
            public int? UpdatedBy { get; set; }

            public DateTime? DeletedAt { get; set; }
            public int? DeletedBy { get; set; }

            public bool IsActive { get; set; } = true;
        }
    

}
