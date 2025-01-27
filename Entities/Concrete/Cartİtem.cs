using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cartİtem : BaseEntity, IEntity
    {
        public int Id { get; set; } // Benzersiz kimlik (Primary Key)
        public int CartId { get; set; } // Hangi sepete ait
        public int ProductId { get; set; } // Ürün ID'si
        public string ProductName { get; set; }
        public string Color { get; set; }
        public SizeEnum Size { get; set; }
        public int Quantity { get; set; } // Sepette kaç adet var

    }
}
