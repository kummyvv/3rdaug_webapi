using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SideMenu
{
    /// <summary>
    /// Model for Package List of under Solution menu
    /// </summary>
    public class PackageList
    {
        public int PACKAGE_ID { get; set; }
        public string PACKAGE_NAME { get; set; }
        public int SOLUTION_ID { get; set; }
        public IEnumerable<ConfigurationList> Configurations {get;set;}
    }
}
