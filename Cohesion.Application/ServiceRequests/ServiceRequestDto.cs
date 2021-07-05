using Cohesion.Domain.ServiceRequests;
using System;

namespace Cohesion.Application.ServiceRequests
{
    public sealed class ServiceRequestDto
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public CurrentStatus CurrentStatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}