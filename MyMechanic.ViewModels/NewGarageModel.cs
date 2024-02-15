using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.ViewModels
{
    public class NewGarageModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public long City { get; set; }

        public long State { get; set; }

        public TimeSpan StartingTime { get; set; }

        public TimeSpan EndingTime { get; set; }

        public string ExtraDescription { get; set; } = string.Empty;

        public List<ServiceCharge> Charges { get; set; }
    }

    public class ServiceCharge
    {
        public int ChargeId { get; set; }

        public decimal Price { get; set; }
    }
}
