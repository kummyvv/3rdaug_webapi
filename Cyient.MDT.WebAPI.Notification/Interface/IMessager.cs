using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Cyient.MDT.WebAPI.Notification.Product;
namespace Cyient.MDT.WebAPI.Notification.Interface
{
    public interface IMessager
    {
        string SendNotification(SendMailRequest sendMailRequest);
    }
}
