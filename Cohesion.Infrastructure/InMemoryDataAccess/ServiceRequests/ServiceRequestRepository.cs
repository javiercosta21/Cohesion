using Cohesion.Application.ServiceRequests;
using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly DBContext _dBContext;

        public ServiceRequestRepository(DBContext dBContext) => _dBContext = dBContext;

        public List<ServiceRequest> GetAll() => _dBContext.serviceRequests;

        public ServiceRequest GetById(Guid id)
        {
            ServiceRequest serviceRequest = _dBContext.serviceRequests.First(x => x.Id == id);
            return serviceRequest;
        }

        public void PostServiceRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.FirstOrDefault(u => u.Id == serviceRequest.Id);
            if (serviceRequestsItem == null) { _dBContext.serviceRequests.Add(serviceRequest); }
        }


        public void PutServiceRequestById(ServiceRequest serviceRequest)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.First(u => u.Id == serviceRequest.Id);

            if ((int)serviceRequest.CurrentStatusCode == 3)
            { SendEmail(serviceRequest.Id.ToString()); }
            serviceRequestsItem.BuildingCode = serviceRequest.BuildingCode;
            serviceRequestsItem.Description = serviceRequest.Description;
            serviceRequestsItem.CurrentStatusCode = serviceRequest.CurrentStatusCode;
            serviceRequestsItem.CreatedBy = serviceRequest.CreatedBy;
            serviceRequestsItem.CreatedDate = serviceRequest.CreatedDate;
            serviceRequestsItem.LastModifiedBy = serviceRequest.LastModifiedBy;
            serviceRequestsItem.LastModifiedDate = serviceRequest.LastModifiedDate;
        }

        public void DeleteById(Guid id)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.First(u => u.Id == id);
            _dBContext.serviceRequests.RemoveAll(x => x.Id == serviceRequestsItem.Id);
        }

        private void SendEmail(string serviceRequestId)
        {
            String FROM = "jcostap2012@gmail.com";
            String FROMNAME = "Sender Name";

            String TO = "javiercosta21@gmail.com";

            String SMTP_USERNAME = "smtp_username";

            String SMTP_PASSWORD = "smtp_password";

            String CONFIGSET = "ConfigSet";

            String HOST = "email-smtp.us-west-2.amazonaws.com";

            int PORT = 587;

            String SUBJECT = "Service Request Closed. ID: " + serviceRequestId;

            String BODY =
                "<h1>Cohesion</h1>" +
                "<p>This email was sent through the " +
                "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface ";


            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;

            message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {

                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);


                client.EnableSsl = true;


                try
                {
                    Console.WriteLine("Attempting to send email...");
                    client.Send(message);
                    Console.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }
        }
    }
}