using System;
namespace OSBOX.Common.Controllers
{
    public interface IEmailController
    {
        bool SendEmailUsingGoogleSMTP(string EmailSubject, string EmailBody, string ToEmailAddress);
    
    }
}
