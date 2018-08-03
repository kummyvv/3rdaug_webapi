using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.BasicInput
{
    /// <summary>
    /// This is model for Basic Input 
    /// </summary>
    public class BasicInput
    {
        //public int BASIC_INPUT_OPTN_ID { get; set; }
        //public string INPUT_OPT_NAME { get; set; }
        public int BASIC_INPUT_ID { get; set; }
        public string BASIC_INPUT_NAME { get; set; }
        public IEnumerable<BasicInputOptions> BasicInputOptions { get; set; }
    }
}
