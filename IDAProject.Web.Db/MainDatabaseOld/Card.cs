using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Card
    {
        public Card()
        {
            CardRebates = new HashSet<CardRebate>();
            EmployeeCards = new HashSet<EmployeeCard>();
            FuelConsumptions = new HashSet<FuelConsumption>();
        }

        public int Id { get; set; }
        public string? CardNumber { get; set; }
        public int? Pin { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? Discount { get; set; }
        public int? CardTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual CardType? CardType { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<CardRebate> CardRebates { get; set; }
        public virtual ICollection<EmployeeCard> EmployeeCards { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
    }
}
