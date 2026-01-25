using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class Wallet
    {
        [Key]
        public int Wid { get; set; }

        [Required]
        public int Uid { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBal { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
