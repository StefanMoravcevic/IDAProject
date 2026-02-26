using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Employees
{
    public class FamilyMemberDto : SaveFamilyMemberRequestModel
    {
        public FamilyMemberDto()
        {
        }
        #region Basic data


        public string? Relationship { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        
        //public string BirthDateFormatted
        //{
        //    get { return DisplayFormatHelpers.FormatDate(BirthDate); }
        //}

        #endregion

    }
}
