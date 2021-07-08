using Cohesion.Application.ServiceRequests;
using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly DBContext _dBContext;

        public ServiceRequestRepository(DBContext dBContext) => _dBContext = dBContext;

        public async Task<List<ServiceRequest>> GetAll() 
        { 
            return await Task.FromResult(_dBContext.serviceRequests); 
        }

        public async Task<ServiceRequest> GetById(Guid id)
        {
            ServiceRequest serviceRequest = _dBContext.serviceRequests.First(x => x.Id == id);
            return await Task.FromResult(serviceRequest);
        }

        public async Task PostServiceRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.FirstOrDefault(u => u.Id == serviceRequest.Id);
            if (serviceRequestsItem == null) { _dBContext.serviceRequests.Add(serviceRequest); }
            await Task.CompletedTask;
        }


        public async Task PutServiceRequestById(ServiceRequest serviceRequest)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.First(u => u.Id == serviceRequest.Id);
                        
            serviceRequestsItem.BuildingCode = serviceRequest.BuildingCode;
            serviceRequestsItem.Description = serviceRequest.Description;
            serviceRequestsItem.CurrentStatusCode = serviceRequest.CurrentStatusCode;
            serviceRequestsItem.CreatedBy = serviceRequest.CreatedBy;
            serviceRequestsItem.CreatedDate = serviceRequest.CreatedDate;
            serviceRequestsItem.LastModifiedBy = serviceRequest.LastModifiedBy;
            serviceRequestsItem.LastModifiedDate = serviceRequest.LastModifiedDate;

            await Task.CompletedTask;
        }

        public async Task DeleteById(Guid id)
        {
            ServiceRequest serviceRequestsItem = _dBContext.serviceRequests.First(u => u.Id == id);
            _dBContext.serviceRequests.RemoveAll(x => x.Id == serviceRequestsItem.Id);

            await Task.CompletedTask;
        }

    }
}