using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Common
{
    public enum MessageCode
    {
        Void = 0,
        Success = 1,
        Failed = 5,
        TechnicalError = 6
    }
    public enum TransactionType
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }
    public class MDTTransactionInfo
    {
        
        public System.Net.HttpStatusCode status { get; set; }
        public MessageCode msgCode { get; set; }
        public string message { get; set; }
        public Object transactionObject { get; set; }
        public int LineNumber { get; set; }
        public string ProcedureName { get; set; }
        
    }
}
