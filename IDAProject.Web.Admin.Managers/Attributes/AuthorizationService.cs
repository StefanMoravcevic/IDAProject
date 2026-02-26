using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Models.General.Enums;

namespace IDAProject.Web.Admin.Managers.Attributes
{

    public class AuthorizationService
    {
        // You can inject dependencies like user roles, features, etc., or fetch them from a database

        public bool CheckUser(UserAccount user, int roleId, int featureId)
        {
            var roleName = Enum.GetName(typeof(AspNetRoles), roleId);
            var featureName = Enum.GetName(typeof(AspNetFeatures), featureId);

            if (roleName != "" && roleName != "Any" && (featureName == "" || featureName == "Any"))
            {
                if (user.Roles.Contains(roleName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (featureName != "" && featureName != "Any" && (roleName == "" && roleName == "Any"))
            {
                if (user.Features.Contains(featureName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (roleName != "" && roleName != "Any" && featureName != "" && featureName != "Any")
            {
                if (user.Roles.Contains(roleName) && user.Features.Contains(featureName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

    }
}