using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cohesion.Application.ServiceRequests
{
    public interface IServiceRequestRepository
    {
        Task<List<ServiceRequest>> GetAll();
        Task<ServiceRequest> GetById(Guid id);
        Task PostServiceRequest(ServiceRequest serviceRequest);
        Task PutServiceRequestById(ServiceRequest serviceRequest);
        Task DeleteById(Guid id);
    }
}
