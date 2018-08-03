using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cyient.MDT.Infrastructure.Concrete.Account;
namespace Cyient.MDT.WebAPI.WebAPISecurity
{
    public class APISecurity
    {
        /// <summary>
        /// Validating if user is valid or not
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValid(string username,string password)
        {
            UserAccountConcrete _service = new UserAccountConcrete();
            if (_service.IsValid(username, password))
                return true;
            else
                return false;
             
        }
    }
}