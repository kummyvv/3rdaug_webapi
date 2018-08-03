using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.Account
{
    /// <summary>
    /// This is the model class to send login details into database to validate the correct login
    /// </summary>
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
