using IDAProject.Web.Admin.Models.Accounts;

namespace IDAProject.Web.Admin.Models.ViewModels
{
    public class NavigationBaseViewModel : BaseViewModel
    {
        private string _navMenuOptionSelector;
        private UserAccount? _userAccount;

        public NavigationBaseViewModel() : base()
        {
            _navMenuOptionSelector = string.Empty;
            _userAccount = null;
        }


        public string NavMenuOptionSelector
        {
            get { return _navMenuOptionSelector; }
            set { _navMenuOptionSelector = value; }
        }

        public UserAccount User
        {
            get { return _userAccount!; }
            set { _userAccount = value; }
        }
    }
}
