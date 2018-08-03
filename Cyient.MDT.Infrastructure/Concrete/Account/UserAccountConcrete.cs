using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using Cyient.MDT.WebAPI.Core.Repository.Account;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Notification.Product;
using Cyient.MDT.WebAPI.Notification.ConcreteProduct;
using Cyient.MDT.WebAPI.Notification.Interface;
using Cyient.MDT.WebAPI.Notification;
using System.Net;

namespace Cyient.MDT.Infrastructure.Concrete.Account
{
    public class UserAccountConcrete : IUserAccount
    {
        public UserAccountConcrete() { }

        /// <summary>
        /// This is login method and it will connect to DB and check if user is exists or not. If user exists then it will display the user detail
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public MDTTransactionInfo Login(UserLogin userLogin)
        {

            MDTTransactionInfo mdt = null;
            UserLoginDetails loginDetails = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter email = new SqlParameter("@email", userLogin.UserName);
            prm.Add(email);
            SqlParameter pwd = new SqlParameter("@pwd", userLogin.Password);
            prm.Add(pwd);

            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getLoginDetails, out StatusValue, prm);
            DataTable dt;
            if (StatusValue == 1)
            {
                dt = ds.Tables[0];
                mdt = new MDTTransactionInfo();
                if (dt.Rows.Count > 0)
                {
                    loginDetails = new UserLoginDetails();
                    loginDetails.USER_ID = Convert.ToInt32(dt.Rows[0]["USER_ID"]);
                    loginDetails.FIRST_NAME = dt.Rows[0]["FIRST_NAME"].ToString();
                    loginDetails.LAST_NAME = dt.Rows[0]["LAST_NAME"].ToString();
                    loginDetails.EMAIL_ADDRESS = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
                    loginDetails.FORCE_PWD_CHNG = Convert.ToBoolean(dt.Rows[0]["FORCE_PWD_CHNG"]);
                    loginDetails.PHOTO = dt.Rows[0]["PHOTO"].ToString();
                    loginDetails.ROLE_NAME = dt.Rows[0]["ROLE_NAME"].ToString();
                    loginDetails.ROLE_ID = Convert.ToInt32(dt.Rows[0]["ROLE_ID"]);
                    loginDetails.UserKey = SecurityEncryptDecrypt.Encrypt(dt.Rows[0]["EMAIL_ADDRESS"].ToString());
                    loginDetails.UserValue = SecurityEncryptDecrypt.Encrypt(dt.Rows[0]["PASSWORD"].ToString());

                }
                mdt.status = HttpStatusCode.OK;
                mdt.transactionObject = loginDetails;
                mdt.msgCode = MessageCode.Success;
                mdt.message = "Login Successfully";
            }
            else if (StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
                //ErrorInfoFromSQL eInfo = null;
                //if (dt.Rows.Count > 0)
                //{
                //    eInfo = new ErrorInfoFromSQL();
                //    eInfo = DatabaseSettings.GetError(dt);
                //    mdt.status = HttpStatusCode.BadRequest;
                //    mdt.transactionObject = eInfo;
                //    mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
                //    mdt.message = eInfo.ErrorMessage;
                //    mdt.LineNumber = eInfo.ErrorLineNo;
                //    mdt.ProcedureName = eInfo.Procedure;
                //}
            }
            return mdt;
        }

        /// <summary>
        /// Change password method which will interact with database and change the password
        /// </summary>
        /// <param name="changePassword">You need to pass ChangePassword type object to process the request</param>
        /// <returns></returns>
        public MDTTransactionInfo ChangePassword(ChangePassword changePassword)
        {
            MDTTransactionInfo mdt = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter email = new SqlParameter("@email", changePassword.Email);
            prm.Add(email);

            SqlParameter oldPwd = new SqlParameter("@oldPwd", changePassword.OldPassword);
            prm.Add(oldPwd);

            SqlParameter NewPwd = new SqlParameter("@newPwd", changePassword.NewPassword);
            prm.Add(NewPwd);

            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.UpdatePassword, out StatusValue, prm);

            mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);

            //if (StatusValue == 1)
            //{
            //    mdt.status = HttpStatusCode.OK;
            //}
            //else if (StatusValue == 5 || StatusValue == 6)
            //{
            //    mdt.status = HttpStatusCode.BadRequest;
            //}
            //if (dt.Rows.Count == 1)
            //{
            //    ErrorInfoFromSQL eInfo = DatabaseSettings.GetError(dt);
            //    mdt.transactionObject = eInfo;
            //    mdt.message = eInfo.ErrorMessage;
            //    mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
            //    mdt.LineNumber = eInfo.ErrorLineNo;
            //    mdt.ProcedureName = eInfo.Procedure;
            //}
            return mdt;
        }

        /// <summary>
        /// It will auto generate new random password and send to user on his email.
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        public MDTTransactionInfo ForgotPassword(ForgotPassword forgotPassword)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                List<SqlParameter> prm = new List<SqlParameter>();
                SqlParameter email = new SqlParameter("@email", forgotPassword.Email);
                prm.Add(email);
                SqlParameter status = new SqlParameter("@Status", 0);
                status.Direction = ParameterDirection.Output;
                prm.Add(status);
                int StatusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.ForgotPassword, out StatusValue, prm);

                DataTable dt = ds.Tables[0];

                //ErrorInfoFromSQL eInfo = null;
                if (StatusValue == 1)
                {
                    mdt.status = HttpStatusCode.OK;
                    mdt.msgCode = MessageCode.Success;
                    mdt.message = "Password has been reset and sent to your registered email.";
                }
                else if (StatusValue == 5 || StatusValue == 6)
                {
                    mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
                }
                //if (dt.Rows.Count == 1)
                //{
                //    eInfo = DatabaseSettings.GetError(dt);
                //    mdt.transactionObject = eInfo;
                //    mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
                //    mdt.message = eInfo.ErrorMessage;
                //    mdt.LineNumber = eInfo.ErrorLineNo;
                //    mdt.ProcedureName = eInfo.Procedure;
                //}
                // If above call success then sending an email to user with latest password.
                ErrorInfoFromSQL eInfo = null;
                if (StatusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        SendMailRequest sendMailRequest = new SendMailRequest();
                        sendMailRequest.recipient = dt.Rows[0]["Email"].ToString();
                        sendMailRequest.subject = "MDT Password Reset";
                        sendMailRequest.body = "Dear User," + Environment.NewLine + "Your password has been reset successfully. please login with new password given below " + Environment.NewLine + "New Password : " + dt.Rows[0]["New Password"].ToString();
                        IMessager messager = new Email();
                        var Notification = new Notification(messager);
                        mdt.message = Notification.DoNotify(sendMailRequest);
                    }
                    dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        eInfo = new ErrorInfoFromSQL();
                        eInfo = DatabaseSettings.GetError(dt);
                        mdt.status = HttpStatusCode.OK;
                        mdt.transactionObject = eInfo;
                        mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
                        mdt.message = eInfo.ErrorMessage;
                        mdt.LineNumber = eInfo.ErrorLineNo;
                        mdt.ProcedureName = eInfo.Procedure;
                    }
                }
                return mdt;
            }
            catch (Exception ex)
            {
                mdt = null;
                throw new Exception(ex.Message);
            }
            finally
            {

            }

        }

        /// <summary>
        /// This will update the user profile pic path into DB after upload.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public MDTTransactionInfo UploadProfilePic(int UserID, string filePath)
        {
            MDTTransactionInfo mdt = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter userid = new SqlParameter("@UserID", UserID);
            prm.Add(userid);
            SqlParameter filepath = new SqlParameter("@ProfilePic", filePath);
            prm.Add(filepath);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.UploadProfilePic, out StatusValue, prm);
            UploadProfilePics uploadProfilePics = new UploadProfilePics();
            if (StatusValue == 1)
            {
                mdt = new MDTTransactionInfo();
                uploadProfilePics.ProfilePicPath = filePath;
                mdt.transactionObject = uploadProfilePics;
                mdt.status = HttpStatusCode.OK;
                mdt.msgCode = MessageCode.Success;
                mdt.message = "File uploaded successfully";
            }
            else if (StatusValue == 5 || StatusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            }
            //if (dt.Rows.Count == 1)
            //{
            //    ErrorInfoFromSQL eInfo = DatabaseSettings.GetError(dt);
            //    mdt.transactionObject = eInfo;
            //    mdt.message = eInfo.ErrorMessage;
            //    mdt.msgCode = (eInfo.Status == 1) ? MessageCode.Success : (eInfo.Status == 6) ? MessageCode.TechnicalError : MessageCode.Failed;
            //    mdt.LineNumber = eInfo.ErrorLineNo;
            //    mdt.ProcedureName = eInfo.Procedure;
            //}
            return mdt;
        }

        /// <summary>
        /// It will check if login user is valid user or not
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValid(string username, string password)
        {

            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter userid = new SqlParameter("@username", username);
            prm.Add(userid);
            SqlParameter filepath = new SqlParameter("@password", password);
            prm.Add(filepath);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int StatusValue = 0;
            bool IsVaild = false;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.CheckLogin, out StatusValue, prm);

            if (StatusValue == 1)
            {
                IsVaild = true;
            }
            else if (StatusValue == 0)
            {
                IsVaild = false;
            }
            //else if (StatusValue == 6)
            //{
            //    //ErrorInfoFromSQL eInfo = DatabaseSettings.GetError(dt);
            //    //mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
            //    IsVaild = false;
            //}
            return IsVaild;
        }
    }
}
