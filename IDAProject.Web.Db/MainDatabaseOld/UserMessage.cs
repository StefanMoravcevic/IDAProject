using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; } = null!;

        public virtual AspNetUser UserFromNavigation { get; set; } = null!;
        public virtual AspNetUser UserToNavigation { get; set; } = null!;
    }
}
