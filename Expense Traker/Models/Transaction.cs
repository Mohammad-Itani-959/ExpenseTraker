using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Traker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        //Every transaction related to a category
        [Range(1, int.MaxValue,ErrorMessage ="Please select category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public int Amount { get; set; }
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitle { get { 
            return Category == null ? "" : Category.Icon+" "+Category.Title;
            }    }

        [NotMapped]
        public string? FormattedAmount { get {
                return ((Category == null || Category.Type == "Expense") ? "-" : "+") + Amount.ToString("c0");
            } }

    }
}
