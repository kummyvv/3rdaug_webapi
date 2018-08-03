using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Common
{
    public class ErrorInfoFromSQL
    {
        public int Status { get; set; }
        public int ErrorNumber { get; set; }
        public string Procedure { get; set; }
        public int ErrorLineNo { get; set; }

        public string TechnicalError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
