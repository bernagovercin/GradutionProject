using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Notification : IEntity
    {
        public int Id { get; set; } // Benzersiz kimlik (Primary Key)
        public string Title { get; set; }
        public int RelatedEntityId { get; set; } // İlgili entity (OrderId, ErrorLogId, vb.)
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationType { get; set; }
    }
}
