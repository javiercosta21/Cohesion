using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;

namespace Cohesion.Application.ServiceRequests
{
    public interface IServiceRequestAppService
    {
        List<ServiceRequest> GetServiceRequests();
        ServiceRequest GetServiceRequestById(Guid id);
        ServiceRequestResult CreateServiceRequest(ServiceRequestDto input);
        ServiceRequestResult EditServiceRequest(Guid originalId, ServiceRequestDto input);
        void DeleteServiceRequestById(Guid id);
    }
}