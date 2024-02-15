using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Appointment Appointment { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(45)]
        public string BillingName { get; set; } = null!;

        public string BillingAddress { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public City City { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal DiscountPr { get; set; }
        
        public decimal TotalWithTax { get; set; }

        public string PaymentStatus { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set;}
    }
}
