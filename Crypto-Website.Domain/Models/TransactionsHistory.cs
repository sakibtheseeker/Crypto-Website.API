using Crypto_Website.Domain.Models;
using System.ComponentModel.DataAnnotations;

public class TransactionsHistory
{
    [Key]
    public int Tid { get; set; }

    public int Uid { get; set; }
    public int Cid { get; set; }   

    public string TransactionType { get; set; }
    public string TransactionStatus { get; set; }

    public decimal Price { get; set; }
    public decimal Quantity { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }

    public bool IsActive { get; set; } = true;

    public User User { get; set; }
    public Crypto Crypto { get; set; }
}
