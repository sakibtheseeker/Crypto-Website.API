using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public int RtId { get; set; }
        public int Uid { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; } = null!;
    }

}
