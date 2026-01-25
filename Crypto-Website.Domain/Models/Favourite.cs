using System;
using System.ComponentModel.DataAnnotations;

namespace Crypto_Website.Domain.Models
{
    public class Favourite
    {
        [Key]
        public int Fid { get; set; }

        public int Uid { get; set; }
        public int Cid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;


        public User User { get; set; }
        public Crypto Crypto { get; set; }
    }
}
