using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cyient.MDT.WebAPI.Core.Common;
using System.Net;

namespace Cyient.MDT.Infrastructure
{
    public class DatabaseSettings
    {
        private static SqlConnection mdtConn = null;

        /// <summary>
        /// Creating Database connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection ConnectToDB()
        {
            string ConnString = ConfigurationManager.ConnectionStrings["MDTConn"].ToString();
            mdtConn = new SqlConnection();
            mdtConn.ConnectionString = ConnString;

            if (mdtConn.State == ConnectionState.Closed)
                mdtConn.Open();

            return mdtConn;
        }


        /// <summary>
        /// Closing database connection
        /// </summary>
        public static void Disconnect()
        {
            if (mdtConn.State == ConnectionState.Open)
                mdtConn.Close();
        }

        /// <summary>
        /// It will return Dataset based on the parameter passed in this method. It is a common method to get dataset for each functionality
        /// </summary>
        /// <param name="sql">This will hold the SQL query/stored procedure and this a mandatory parameter</param>
        /// <param name="parameter">This is a option parameter which will holds the list of parameters passed in stored procedure</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, IList<SqlParameter> parameter = null)
        {
            SqlCommand command = new SqlCommand(sql, ConnectToDB());
            command.CommandType = CommandType.StoredProcedure;
            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    command.Parameters.Add(p);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Disconnect();
            return ds;
        }

        /// <summary>
        /// It will return Dataset based on the parameter passed in this method. It is a common method to get dataset for each functionality with Status. Status will identify whether request is success or any error
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Status"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>

        public static DataSet GetDataSet(string sql, out int Status, IList<SqlParameter> parameter = null)
        {

            SqlCommand command = new SqlCommand(sql, ConnectToDB());
            command.CommandType = CommandType.StoredProcedure;
            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    command.Parameters.Add(p);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Status = Convert.ToInt32(command.Parameters["@Status"].Value);
            Disconnect();
            return ds;
        }

        /// <summary>
        /// It will execute the query like insert/update/delete or any kind of statement which will not return dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Status"></param>
        /// <param name="parameter"></param>

        public static void ExecuteNonQuery(string sql, out int Status, IList<SqlParameter> parameter = null)
        {

            SqlCommand command = new SqlCommand(sql, ConnectToDB());
            command.CommandType = CommandType.StoredProcedure;
            if (parameter != null)
            {
                foreach (var p in parameter)
                {
                    command.Parameters.Add(p);
                }
            }
            command.ExecuteNonQuery();
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            Status = Convert.ToInt32(command.Parameters["@Status"].Value);
            Disconnect();

        }

        /// <summary>
        /// It will bind the error if any error is there in Stored Proc
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static ErrorInfoFromSQL GetError(DataTable dt)
        {
            ErrorInfoFromSQL eInfo = new ErrorInfoFromSQL();
            if (dt.Rows.Count > 0)
            {
                eInfo.Status = Convert.ToInt32(dt.Rows[0][0]);
                eInfo.ErrorNumber = Convert.ToInt32(dt.Rows[0][0]);
                eInfo.Procedure = dt.Rows[0]["ErrorProcedure"].ToString();
                eInfo.ErrorLineNo = Convert.ToInt32(dt.Rows[0]["ErrorLine"]);
                eInfo.TechnicalError = dt.Rows[0]["TechnicalError"].ToString();
                eInfo.ErrorMessage = dt.Rows[0]["ErrorMessage"].ToString();
            }

            return eInfo;
        }

        /// <summary>
        /// It will return mdt object with fail/pass based on Status
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Status"></param>
        /// <param name="message"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static MDTTransactionInfo GetTransObject(object obj, int Status, string message, DataSet ds = null)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            if (Status == 1)
            {
                mdt.transactionObject = obj;
                mdt.status = HttpStatusCode.OK;
                mdt.msgCode = MessageCode.Success;
                mdt.message = message;
            }
            else if (Status == 5 || Status == 6)
            {
                ErrorInfoFromSQL eInfo = null;
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        eInfo = new ErrorInfoFromSQL();
                        eInfo = GetError(dt);
                        mdt.status = HttpStatusCode.BadRequest;
                        mdt.transactionObject = eInfo;
                        mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
                        mdt.message = eInfo.ErrorMessage;
                        mdt.LineNumber = eInfo.ErrorLineNo;
                        mdt.ProcedureName = eInfo.Procedure;
                    }
                }
            }
            return mdt;
        }

        /// <summary>
        /// It will return the parameters list with associated values to supply into the sql command
        /// </summary>
        /// <param name="prmList"></param>
        /// <param name="prmValuesList"></param>
        /// <returns></returns>
        public static List<SqlParameter> BindParamers(string prmNamesList = "" ,string prmValuesList = "")
        {
            string[] PrmNames= new string[] { }; //= (prmNamesList.Length>0)?prmNamesList.Split(',');
            string[] PrmValues = new string[] { };//= (prmValuesList.Length>0)?prmValuesList.Split(',');

            if (prmNamesList.Length > 0)
                PrmNames = prmNamesList.Split(',');

            if (prmValuesList.Length > 0)
                PrmValues = prmValuesList.Split(',');

            List<SqlParameter> prmList = new List<SqlParameter>();
            SqlParameter prm;

            for(int i=0;i<PrmNames.Length; i++)
            {
                prm = new SqlParameter(PrmNames[i], PrmValues[i]);
                prmList.Add(prm);
            }
            prm = new SqlParameter("@Status", 0);
            prm.Direction = ParameterDirection.Output;

            prmList.Add(prm);
            return prmList;

        }

    }

}
