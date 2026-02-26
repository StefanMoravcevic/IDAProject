using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.Accounts
{
    public class UserAccount : ISelectOption
    {
        private int _id;
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private int _employeeId;
        private int _partnerId;
        private int _orgId;
        private int _printerId;
        private int _companyId;
        private string _companyName;
        private string _userCulture;
        private readonly List<string> _roles;
        private readonly List<string> _features;

        public UserAccount()
        {
            _id = 0;
            _employeeId = 0;
            _partnerId = 0;
            _orgId = 0;
            _printerId = 0;
            _username = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _email = string.Empty;
            _userCulture = string.Empty;
            _companyName = string.Empty;
            _roles = new List<string>();
            _features = new List<string>();
            _companyId = 0;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        public int PartnerId
        {
            get { return _partnerId; }
            set { _partnerId = value; }
        }
        public int OrgId
        {
            get { return _orgId; }
            set { _orgId = value; }
        }

        public int PrinterId
        {
            get { return _printerId; }
            set { _printerId = value; }
        }

        public string UserCulture
        {
            get { return _userCulture; }
            set { _userCulture = value; }
        }

        public List<string> Roles
        {
            get { return _roles; }
        }

        public List<string> Features
        {
            get { return _features; }
        }

        public int? Value
        {
            get { return _id; }
        }

        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        public string Description
        {
            get
            {
                return $"{_firstName} {_lastName} - [{_companyName}]";
            }
        }
    }

}
