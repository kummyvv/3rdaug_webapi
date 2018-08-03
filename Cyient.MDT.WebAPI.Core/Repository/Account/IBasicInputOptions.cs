using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Entities.BasicInput;
using Cyient.MDT.WebAPI.Core.Common;
namespace Cyient.MDT.WebAPI.Core.Repository.BasicInput
{
    public interface IBasicInputOptions
    {
        MDTTransactionInfo GetBasicInputs();
    }
}
