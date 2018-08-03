using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Common;
namespace Cyient.MDT.WebAPI.Core.Repository.SideMenu
{
    public interface ISideMenu
    {
        MDTTransactionInfo GetSideMenu(int UserID);
    }
}
