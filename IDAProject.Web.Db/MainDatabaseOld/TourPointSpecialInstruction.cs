using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourPointSpecialInstruction
    {
        public int Id { get; set; }
        public int TourPointId { get; set; }
        public string Instruction { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual TourPoint TourPoint { get; set; } = null!;
    }
}
