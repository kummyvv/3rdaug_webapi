using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Common;
namespace Cyient.MDT.WebAPI.Core.Repository.SubSystem
{
    /// <summary>
    /// This interface will have all method define here related to Sub System view in UI
    /// </summary>
    public interface ISubSystem
    {
        /// <summary>
        /// It will get the Sub System view details for Package
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        MDTTransactionInfo GetPackageSystemDetails(int PackageID);
    }
}
