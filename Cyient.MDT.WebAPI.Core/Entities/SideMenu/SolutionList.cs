using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SideMenu
{
    /// <summary>
    /// Model for Solution List of Top Level menu
    /// </summary>
    public class SolutionList
    {
        public int SOLUTION_ID { get; set; }
        public string SOLUTION_NAME { get; set; }
        public IEnumerable<PackageList> Packages { get; set; }
    }
}
