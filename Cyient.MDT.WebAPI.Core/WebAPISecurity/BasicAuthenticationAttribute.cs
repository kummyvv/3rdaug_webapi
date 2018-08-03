using System;
using System.Net;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Text;
using Cyient.MDT.WebAPI.Core.Common;
namespace Cyient.MDT.WebAPI.WebAPISecurity
{
    public class BasicAuthenticationAttribute :AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // Gets header parameters  
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                // Gets username and password  
                string usrename = SecurityEncryptDecrypt.Decrypt(originalString.Split(':')[0]);
                string password = SecurityEncryptDecrypt.Decrypt(originalString.Split(':')[1]);

                // Validate username and password  
                if (!WebAPISecurity.APISecurity.IsValid(usrename, password))
                {
                    // returns unauthorized error  
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

            base.OnAuthorization(actionContext);
        }
    }
}