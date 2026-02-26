using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DocumentSerieType
    {
        public DocumentSerieType()
        {
            DocumentSeries = new HashSet<DocumentSeries>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<DocumentSeries> DocumentSeries { get; set; }
    }
}
