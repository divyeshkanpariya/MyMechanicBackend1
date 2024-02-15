using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class GarageAvailServiceModel
    {
        public int Id { get; set; }

        public long GarageId { get; set; }

        public int ServiceTypeId { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
