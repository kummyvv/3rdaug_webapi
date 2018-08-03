using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Cyient.MDT.WebAPI.Core.Repository.SubSystem;
using Cyient.MDT.WebAPI.Core.Entities.SubSystem;
using Cyient.MDT.WebAPI.Core.Common;

namespace Cyient.MDT.Infrastructure.Concrete.PackageSystems
{
    public class PackageSystemConcrete : ISubSystem
    {
        public PackageSystemConcrete() { }

        
        /// <summary>
        /// It will return the Sub System details in landing page for sales based on Package ID
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        public MDTTransactionInfo GetPackageSystemDetails(int PackageID)
        {

            MDTTransactionInfo mdt = new MDTTransactionInfo();
            IEnumerable<PackageSystem> PackageSystems = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter package_Id = new SqlParameter("@PackageID", PackageID);
            prm.Add(package_Id);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getPackageSystemDetails, out StatusValue, prm);

            DataTable dt;
            if (StatusValue == 1)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    PackageSystems = from d in dt.AsEnumerable()
                                     select new PackageSystem
                                     {
                                         PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                         SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                         SYSTEM_VARIANT_ID = d.Field<int>("SYSTEM_VARIANT_ID"),
                                         DEPENDENT_ID = d.Field<int>("DEPENDENT_ID"),
                                         SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                         TYPE = d.Field<bool>("TYPE"),
                                         COST_TYPE = Convert.ToBoolean(d.Field<int>("COST_TYPE")),
                                         SYSTEM_IMAGE = d.Field<string>("SYSTEM_IMAGE"),
                                         SYSTEM_NAME = d.Field<string>("SYSTEM_NAME"),
                                         EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                         ELECTRICAL_COST = d.Field<double>("ELECTRICAL_COST"),
                                         MECHANICAL_COST = d.Field<double>("MECHANICAL_COST"),
                                         COMMENTS = d.Field<string>("COMMENTS"),
                                         REMARKS = d.Field<string>("REMARKS"),
                                         SystemVariants = GetSystemVariants(d.Field<int>("SYSTEM_VARIANT_ID")).transactionObject as IEnumerable<SystemVariants>
                                     };

                }
                mdt = DatabaseSettings.GetTransObject(PackageSystems, StatusValue, "Package System Details Fetched Successfully", ds);
            }
            else if(StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            }
            
            return mdt;
        }
        private MDTTransactionInfo GetSystemVariants(int VariantID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            IEnumerable<SystemVariants> systemVariants = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter System_Variant_ID = new SqlParameter("@System_Variant_ID", VariantID);
            prm.Add(System_Variant_ID);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getSystemVariants, out StatusValue, prm);
            DataTable dt;
            if (StatusValue == 1)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    systemVariants = from d in dt.AsEnumerable()
                                     select new SystemVariants
                                     {
                                         PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                         SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                         SYSTEM_VARIANT_ID = d.Field<int>("SYSTEM_VARIANT_ID"),
                                         DEPENDENT_ID = d.Field<int>("DEPENDENT_ID"),
                                         SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                         TYPE = d.Field<bool>("TYPE"),
                                         COST_TYPE = Convert.ToBoolean(d.Field<int>("COST_TYPE")),
                                         SYSTEM_IMAGE = d.Field<string>("SYSTEM_IMAGE"),
                                         SYSTEM_NAME = d.Field<string>("SYSTEM_NAME"),
                                         EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                         ELECTRICAL_COST = d.Field<double>("ELECTRICAL_COST"),
                                         MECHANICAL_COST = d.Field<double>("MECHANICAL_COST"),
                                         COMMENTS = d.Field<string>("COMMENTS"),
                                         REMARKS = d.Field<string>("REMARKS")
                                     };
                }
                mdt.msgCode = MessageCode.Success;
                mdt.status = HttpStatusCode.OK;
                mdt.message = "Record found";
                mdt.transactionObject = systemVariants;
            }
            else if (StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            }
            return mdt;
        }
    }
}
