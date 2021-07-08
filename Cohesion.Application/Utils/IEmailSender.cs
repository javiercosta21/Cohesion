using System;
using System.Collections.Generic;
using System.Text;

namespace Cohesion.Application.Utils
{
    public interface IEmailSender
    {
        void SendMail(string serviceRequestId);
    }
}
