using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.Services.Interfaces;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.Infrastructure.Services
{
    public class UserLoginService : IUserLoginService
    {
        UserAccountConcrete _service = new UserAccountConcrete();
        //public async Task<IEnumerable<UserLogin>> Login(UserLogin userLogin)

        public UserLoginService()
        {

        }

       

        MDTTransactionInfo IUserLoginService.Login(UserLogin userLogin)
        {
            try
            {
                return _service.Login(userLogin);
            }
            catch (Exception ex)
            {
                Logger Log = new Logger();
                Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                        "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                return null;//new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }
        }
    }
}
