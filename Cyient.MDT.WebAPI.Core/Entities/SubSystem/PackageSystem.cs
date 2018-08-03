using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SubSystem
{
    /// <summary>
    /// This entity will hold the value of SubSystem details for Package
    /// </summary>
    public class PackageSystem
    {
        public int PACKAGE_ID { get; set; }
        public int SYSTEM_ID { get; set; }
        public int SYSTEM_VARIANT_ID { get; set; }
        public int DEPENDENT_ID { get; set; }
        public bool SELECT { get; set; }
        public bool TYPE { get; set; }
        public bool COST_TYPE { get; set; }
        public string SYSTEM_IMAGE { get; set; }
        public string SYSTEM_NAME { get; set; }
        public double EQUIPMENT_COST { get; set; }
        public double ELECTRICAL_COST { get; set; }
        public double MECHANICAL_COST { get; set; }
        public string COMMENTS { get; set; }
        public string REMARKS { get; set; }

        public IEnumerable<SystemVariants> SystemVariants { get; set; }

    }
}
