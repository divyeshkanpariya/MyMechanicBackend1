using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Entities.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; } = null!;

        [Required]
        public Garage Garage { get; set; } = null!;

        [Required]
        public CarModel CarModel { get; set; } = null!;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(100)]
        public string FuelType { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string CarNo { get; set; } = null!;

        [Required]
        public string OtherDescription { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Status { get; set; } = "PENDING";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<AppointmentService> AppointmentServices { get; set; } = null!;

        public ICollection<Service> Services { get; set; } = null!;

        public ICollection<Invoice> Invoices { get; set; } = null!;
    }
}
