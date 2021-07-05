using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;

namespace Cohesion.Application.ServiceRequests
{
    public class ServiceRequestAppService : IServiceRequestAppService
    {
        private readonly IServiceRequestRepository  _serviceRequestRepository;
        public ServiceRequestAppService(IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        public List<ServiceRequest> GetServiceRequests()
        {
            return _serviceRequestRepository.GetAll();
        }

        public ServiceRequest GetServiceRequestById(Guid id)
        {
            return _serviceRequestRepository.GetById(id);
        }

        public ServiceRequestResult CreateServiceRequest(ServiceRequestDto input)
        {
            var result = ServiceRequest.Create(Guid.NewGuid(),
                 input.BuildingCode,
                 input.Description,
                 input.CurrentStatusCode,
                 input.CreatedBy,
                 input.CreatedDate,
                 input.LastModifiedBy,
                 input.LastModifiedDate);

            if (result.ErrorMessages.Count > 0) return result;

            _serviceRequestRepository.PostServiceRequest(result.Instance);

            return result;
        }

        public ServiceRequestResult EditServiceRequest(Guid originalId, ServiceRequestDto input)
        {
            var result = ServiceRequest.Edit(originalId,
                 input.BuildingCode,
                 input.Description,
                 input.CurrentStatusCode,
                 input.CreatedBy,
                 input.CreatedDate,
                 input.LastModifiedBy,
                 input.LastModifiedDate);

            if (result.ErrorMessages.Count > 0) return result;

            _serviceRequestRepository.PutServiceRequestById(result.Instance);

            return result;
        }

        public void DeleteServiceRequestById(Guid id)
        {
            _serviceRequestRepository.DeleteById(id);
        }

        


    }
}
