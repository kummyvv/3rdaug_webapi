using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.WebAPI.Core.Common;
namespace Cyient.MDT.WebAPI.Core.Repository.Account
{
    public interface IUserAccount
    {
        MDTTransactionInfo Login(UserLogin userLogin);

        MDTTransactionInfo ChangePassword(ChangePassword changePassword);

        MDTTransactionInfo ForgotPassword(ForgotPassword forgotPassword);
        bool IsValid(string username, string password);
    }
}
