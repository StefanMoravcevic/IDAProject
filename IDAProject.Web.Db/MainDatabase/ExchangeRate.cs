using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class ExchangeRate
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public int CurrencyId { get; set; }

    public DateTime CurrencyDate { get; set; }

    public decimal? ExchangeRate1 { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual AspNetUser? DeletedByNavigation { get; set; }
}
