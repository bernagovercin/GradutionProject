using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ErrorReport : BaseEntity, IEntity
    {
        public int Id { get; set; } // Benzersiz kimlik (Primary Key)
        public string Title { get; set; } // Hata başlığı
        public string Description { get; set; } // Hata açıklaması
        public string Severity { get; set; } // Ciddiyet seviyesi (Kritik, Orta, Düşük)
        public string Component { get; set; } // Hata ile ilgili bileşen adı
        public string ResolutionMessage { get; set; } // Hatanın çözüm mesajı
        public string ErrorStatus { get; set; } // Açık, Çözüldü, Kapalı
    }
}
