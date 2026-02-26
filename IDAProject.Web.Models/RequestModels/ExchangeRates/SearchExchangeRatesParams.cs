using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.ExchangeRates
{
    public class SearchExchangeRatesParams
    {
        public int? Id { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime? Date { get; set; }
    }
}
