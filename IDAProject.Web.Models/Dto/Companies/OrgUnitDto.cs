namespace IDAProject.Web.Models.Dto.Companies
{
    public class OrgUnitDto : SaveOrgUnitRequestModel
    {
        public OrgUnitDto()
        {
        }
        #region Basic data

        public string? Company { get; set; }
        public string? ParentOrgUnit { get; set; }

        #endregion
    }
}
