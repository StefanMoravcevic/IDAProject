using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Form
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public int? DocumentTypeId { get; set; }

    public string? TemplateFile { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }
}
