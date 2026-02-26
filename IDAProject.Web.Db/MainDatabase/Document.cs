using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Document
{
    public int Id { get; set; }

    public string RelativeFilePath { get; set; } = null!;

    public int DocumentTypeId { get; set; }

    public int ReferenceId { get; set; }

    public int UploadedByUserId { get; set; }

    public DateTime UploadedDate { get; set; }

    public int SourceId { get; set; }

    public bool IsDeleted { get; set; }

    public string DownloadFileName { get; set; } = null!;

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual DocumentsSource Source { get; set; } = null!;

    public virtual AspNetUser UploadedByUser { get; set; } = null!;
}
