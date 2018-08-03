using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.Account
{
    /// <summary>
    /// This is the model class to return the user details from server to client after login successfully.
    /// </summary>
    public class UserLoginDetails
    {
        public int USER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public bool FORCE_PWD_CHNG { get; set; }
        public string PHOTO { get; set; }
        public string ROLE_NAME { get; set; }
        public int ROLE_ID { get; set; }

        public string UserKey { get; set; }
        public string UserValue { get; set; }
    }
}
