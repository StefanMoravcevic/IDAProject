using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class DocumentSerieType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<DocumentSeries> DocumentSeries { get; set; } = new List<DocumentSeries>();
}
