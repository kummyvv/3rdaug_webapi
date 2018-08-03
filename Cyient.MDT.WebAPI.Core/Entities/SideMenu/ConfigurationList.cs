using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SideMenu
{    /// <summary>
     /// Model for Configuration List of under Package menu
     /// </summary>
    public class ConfigurationList
    {
        public int CONFIGURATION_ID { get; set; }
        public string CONFIGURATION_NAME { get; set; }
        public int PACKAGE_ID { get; set; }
    }
}
