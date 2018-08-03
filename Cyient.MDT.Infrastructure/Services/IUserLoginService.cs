using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;

namespace Cyient.MDT.Services.Interfaces
{
    public interface IUserLoginService
    {
        //Task<IEnumerable<UserLogin>> Login(UserLogin userLogin);
        MDTTransactionInfo Login(UserLogin userLogin);
    }
}
