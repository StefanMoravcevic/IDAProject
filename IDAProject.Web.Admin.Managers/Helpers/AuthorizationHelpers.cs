using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Models.Accounts;

namespace IDAProject.Web.Admin.Managers.Helpers
{
    public class AuthorizationHelpers
    {
        private readonly AuthorizationService _authorizationService;
        public AuthorizationHelpers(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public bool UserHasRights(UserAccount user, int role, int feature)
        {
            if (_authorizationService.CheckUser(user, role, feature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}