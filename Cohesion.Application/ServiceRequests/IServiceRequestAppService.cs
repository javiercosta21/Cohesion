using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cohesion.Application.ServiceRequests
{
    public interface IServiceRequestAppService
    {
        Task<List<ServiceRequest>> GetServiceRequests();
        Task<ServiceRequest> GetServiceRequestById(Guid id);
        Task<ServiceRequestResult> CreateServiceRequest(ServiceRequestDto input);
        Task<ServiceRequestResult> EditServiceRequest(Guid originalId, ServiceRequestDto input);
        Task DeleteServiceRequestById(Guid id);
    }
}