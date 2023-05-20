using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker_App.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;


        [NotMapped]
        public string? CategoryTitleAndIcon
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        [NotMapped]
        public string? AmountFormat
        {
            get
            {
                return ((Category == null || Category.type == "Expense") ? "- " : "+ ") + Amount;
            }
        }
    }
}
