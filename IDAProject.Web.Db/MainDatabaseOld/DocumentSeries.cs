using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DocumentSeries
    {
        public int Id { get; set; }
        public int DocumentSerieTypeId { get; set; }
        public int Year { get; set; }
        public int NextNumber { get; set; }
        public int IncrementSeed { get; set; }
        public string? Pattern { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual DocumentSerieType DocumentSerieType { get; set; } = null!;
    }
}
