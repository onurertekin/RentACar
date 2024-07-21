using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Entities
{
    public class Rezervation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int TotalPrice { get; set; }

        #region Navigation Properties
        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
