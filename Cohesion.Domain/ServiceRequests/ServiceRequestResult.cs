using System.Collections.Generic;

namespace Cohesion.Domain.ServiceRequests
{
    public sealed class ServiceRequestResult
    {
        public List<string> ErrorMessages { get; set; }
        public ServiceRequest Instance { get; set; }
    }
}
