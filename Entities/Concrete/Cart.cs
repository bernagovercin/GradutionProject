using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cart : IEntity
    {
        public int Id { get; set; } // Benzersiz kimlik (Primary Key)
        public int UserId { get; set; } // Hangi kullanıcıya ait
        public List<Cartİtem> CartItems { get; set; } = new List<Cartİtem>();
    }
}
