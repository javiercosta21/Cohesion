using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;

namespace Cohesion.Application.ServiceRequests
{
    public interface IServiceRequestRepository
    {
        List<ServiceRequest> GetAll();
        ServiceRequest GetById(Guid id);
        void PostServiceRequest(ServiceRequest serviceRequest);
        void PutServiceRequestById(ServiceRequest serviceRequest);
        void DeleteById(Guid id);
    }
}
