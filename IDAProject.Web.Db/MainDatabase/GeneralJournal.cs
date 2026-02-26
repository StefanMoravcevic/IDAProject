using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class GeneralJournal
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public int? GeneralJournalNumber { get; set; }

    public int? UserId { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int? RowNumber { get; set; }

    public DateOnly? GeneralJournalDate { get; set; }

    public string? GeneralJournalType { get; set; }

    public int? DocumentNumber { get; set; }

    public int? ExternalDocumentNumber { get; set; }

    public int? PartnerTypeId { get; set; }

    public string? Notes { get; set; }

    public string? KindOfRights { get; set; }

    public string? SourceOfFinance { get; set; }

    public int? CurrencyId { get; set; }

    public int? Amount { get; set; }

    public int? AmountInRsd { get; set; }

    public int? OrgUnitId { get; set; }

    public string? TypeOfClosingDocument { get; set; }

    public int? NumberOfClosingDocument { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual OrgUnit? OrgUnit { get; set; }

    public virtual PartnerType? PartnerType { get; set; }
}
