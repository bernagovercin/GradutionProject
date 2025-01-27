﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Kullanıcı ID'si
        public List<Orderİtem> OrderItems { get; set; } = new List<Orderİtem>(); // Siparişin ürünleri
    }
}
