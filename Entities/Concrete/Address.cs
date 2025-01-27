using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address : BaseEntity, IEntity

    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Header { get; set; } // Adres başlığı (örn: Ev, İş)
        public string Neighborhood { get; set; } // Mahalle
        public string Avenue { get; set; } // Cadde
        public string Street { get; set; } // Sokak
        public int StreetNumber { get; set; } // No
        public int ApartmentNumber { get; set; } // Daire
        public string District { get; set; } // İlçe
        public string City { get; set; } // İl
        public string Country { get; set; } // Ülke
        public string FullAddress { get; set; } // Tam Adres
    }
}
