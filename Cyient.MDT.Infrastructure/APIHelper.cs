using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.Infrastructure
{
    /// <summary>
    /// This an helper class which will contain all the static fields
    /// </summary>
    public static class APIHelper
    {
        public static int StatusValue = 0;

        /// <summary>
        /// Stored Procedure List
        /// </summary>
        public static string getLoginDetails = "sp_LoginUser";
        public static string UpdatePassword = "sp_UpdatePassword";
        public static string getBasicInput = "sp_GetBasicInput";
        public static string getBasicInputDetails = "sp_GetBasicInputDetails";
        public static string ForgotPassword = "sp_ForgotPassword";
        public static string getPackageSystemDetails = "sp_GetPackageSystems";
        public static string getSystemVariants = "sp_GetSystemVariants";
        public static string getSolutions = "sp_GetSolutions";
        public static string getPackageList = "sp_GetPackageList";
        public static string getLatestConfigurations = "sp_GetLatestConfiguration";
        public static string UploadProfilePic = "sp_UpdateProfilePic";
        public static string CheckLogin = "sp_CheckLogin";
        //public static string getLoginDetails                = "sp_LoginUser";
        //public static string getLoginDetails                = "sp_LoginUser";
        //public static string getLoginDetails                = "sp_LoginUser";
        //public static string getLoginDetails                = "sp_LoginUser";

        /// <summary>
        /// Parameters list. Parameters are related to sequencially as per Stored procedure list above.
        /// </summary>
        public static string getLoginDetailsParameters = "@email,@pwd";
        public static string UpdatePasswordParameters = "@email,@oldPwd,@newPwd";
        public static string getBasicInputdParameters = "";
        public static string getBasicInputDetailsParameters = "@BASIC_INPUT_ID";
        public static string ForgotPasswordParameters = "@email";
        public static string getPackageSystemDetailsParameters = "@PackageID";
        public static string getSystemVariantsParameters = "@System_Variant_ID";
        public static string getSolutionsParameters = "";
        public static string getPackageListParameters = "@SolutionID";
        public static string getLatestConfigurationsParameters = "@UserID,@PackageID,@NoofConfiguration";
        public static string UploadProfilePicParameters = "@User_ID,@ProfilePic";
        public static string CheckLoginParameters = "@username,@password";
        //public static string UpdatePasswordParameters = "@email,@pwd";
        //public static string UpdatePasswordParameters = "@email,@pwd";




    }
}
