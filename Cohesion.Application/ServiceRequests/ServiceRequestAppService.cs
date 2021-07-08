using Cohesion.Application.Utils;
using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cohesion.Application.ServiceRequests
{
    public class ServiceRequestAppService : IServiceRequestAppService
    {
        private readonly IServiceRequestRepository  _serviceRequestRepository;
        private readonly IEmailSender _emailSender;
        public ServiceRequestAppService(IServiceRequestRepository serviceRequestRepository, IEmailSender emailSender)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _emailSender = emailSender;
        }

        public async Task<List<ServiceRequest>> GetServiceRequests()
        {
            return await _serviceRequestRepository.GetAll();
        }

        public async Task<ServiceRequest> GetServiceRequestById(Guid id)
        {
            return await _serviceRequestRepository.GetById(id);
        }

        public async Task<ServiceRequestResult> CreateServiceRequest(ServiceRequestDto input)
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

            await _serviceRequestRepository.PostServiceRequest(result.Instance);

            return result;
        }

        public async Task<ServiceRequestResult> EditServiceRequest(Guid originalId, ServiceRequestDto input)
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
            if((int)result.Instance.CurrentStatusCode == 3) { _emailSender.SendMail(result.Instance.Id.ToString()); }
            await _serviceRequestRepository.PutServiceRequestById(result.Instance);

            return result;
        }

        public async Task DeleteServiceRequestById(Guid id)
        {
            await _serviceRequestRepository.DeleteById(id);
        }     


    }
}
