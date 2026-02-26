using Tms.Web.Models.Dto.Common;
using Tms.Web.Models.Dto.Partners;
using Tms.Web.Models.Dto.Payments;

namespace Tms.Web.Models.Dto.Companies
{
    public class PartnerDto
    {
        public PartnerDto()
        {
            PartnerType = new PartnerTypeDto();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? AccountingCode { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public ZipCodeDto? ZipCode { get; set; }
        public StateDto? State { get; set; }
        public CityDto? City { get; set; }
        public PartnerTypeDto PartnerType { get; set; }
        public string? Ein { get; set; }
        public string? Mc { get; set; }
        public string? Dot { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public PaymentConditionDto? PaymentCondition { get; set; }
        public ContactDto? PrimaryContact { get; set; }
        public CostIncomeTypesDto? IncomeType { get; set; }
        public bool? Blocked { get; set; }
        public string? BlockedComment { get; set; }
    }
}
