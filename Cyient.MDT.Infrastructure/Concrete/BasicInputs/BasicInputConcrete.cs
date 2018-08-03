using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Cyient.MDT.WebAPI.Core.Entities.BasicInput;
using Cyient.MDT.WebAPI.Core.Repository.BasicInput;
using Cyient.MDT.WebAPI.Core.Common;
using System.Net;

namespace Cyient.MDT.Infrastructure.Concrete.BasicInputs
{
    public class BasicInputConcrete : IBasicInputOptions
    {

        public BasicInputConcrete() { }
        /// <summary>
        /// This will return list of Basic input options available in database
        /// </summary>
        /// <returns></returns>
        public MDTTransactionInfo GetBasicInputs()
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            IEnumerable<BasicInput> basicInputs = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getBasicInput, out StatusValue, prm);

            DataTable dt;

            if (StatusValue == 1)
            {
                mdt = new MDTTransactionInfo();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    basicInputs = from d in dt.AsEnumerable()
                                  select new BasicInput
                                  {
                                      BASIC_INPUT_ID = d.Field<int>("BASIC_INPUT_ID"),
                                      BASIC_INPUT_NAME = d.Field<string>("BASIC_INPUT_NAME"),
                                      BasicInputOptions = GetBasicInputOptions(d.Field<int>("BASIC_INPUT_ID")).transactionObject as IEnumerable<BasicInputOptions>
                                  };
                }
                if (basicInputs != null)
                {
                    mdt.transactionObject = basicInputs;
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = MessageCode.Success;
                    mdt.message = "Fetched basic input detail successfully";
                }
                else
                {
                    mdt.transactionObject = null;
                    mdt.status = HttpStatusCode.NotFound;
                    mdt.msgCode = MessageCode.Failed;
                    mdt.message = "No record found";
                }
            }
            else if (StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            }
            return mdt;
        }


        private MDTTransactionInfo GetBasicInputOptions(int BasicInputID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            IEnumerable<BasicInputOptions> basicInputOptions = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter Basic_Input_ID = new SqlParameter("@BASIC_INPUT_ID", BasicInputID);
            prm.Add(Basic_Input_ID);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet("sp_GetBasicInputDetails", out StatusValue, prm);

            DataTable dt;
            if (StatusValue == 1)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    basicInputOptions = from d in dt.AsEnumerable()
                                        select new BasicInputOptions
                                        {
                                            BASIC_INPUT_OPTN_ID = d.Field<int>("BASIC_INPUT_OPTN_ID"),
                                            INPUT_OPT_NAME = d.Field<string>("INPUT_OPT_NAME"),
                                            BASIC_INPUT_ID = d.Field<int>("BASIC_INPUT_ID")
                                        };
                }
                mdt.msgCode = MessageCode.Success;
                mdt.status = HttpStatusCode.OK;
                mdt.message = "Record found";
                mdt.transactionObject = basicInputOptions;
            }
            else if (StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            }
            return mdt;
        }
    }
}
