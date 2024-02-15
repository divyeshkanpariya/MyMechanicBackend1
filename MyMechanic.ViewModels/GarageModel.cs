using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class GarageModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public long CityId { get; set; }

        public string City { get; set; } = string.Empty;

        public long StateId { get; set; }

        public string State { get; set; } = string.Empty;

        public TimeSpan StartingTime { get; set; }

        public TimeSpan EndingTime { get; set; }

        public decimal Ratings { get; set; }

        public string ExtraDescription { get; set; } = string.Empty;

        public string Status { get; set; } = "ACTIVE";

        public List<GarageAvailServiceModel> AvailableServices { get; set; } = null!;

        public List<GaragePhotosModel> Photos { get; set; } = null;
    }
}
