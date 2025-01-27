using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; } // Benzersiz kimlik (Primary Key)
        public string Name { get; set; } // Örneğin: T-Shirt, Pantolon
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
