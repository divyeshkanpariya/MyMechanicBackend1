using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class City
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CityName { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        
        public State State { get; set; } = null!;

        //public ICollection<PostalCode> PostalCodes { get; set; } = null!;?

        public ICollection<Garage> Garages { get; set; } = null!;

        public ICollection<Invoice> Invoices { get; set; } = null!;
    }
}
