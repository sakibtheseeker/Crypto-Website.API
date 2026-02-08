using System.ComponentModel.DataAnnotations;

namespace Crypto_Website.Domain.Models
{
    public class Portfolio
    {
        [Key]
        public int Pid { get; set; }
        public int Uid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsActive { get; set; }

        public ICollection<PortfolioAsset> Assets { get; set; }
    }
}
