using Cohesion.Domain.ServiceRequests;
using System;
using System.Collections.Generic;

namespace Cohesion.Infrastructure.InMemoryDataAccess
{
    public class DBContext
    {
        public DBContext()
        {
            SeedData();
        }

        public List<ServiceRequest> serviceRequests { get; set; }

        private void SeedData()
        {
            serviceRequests = new List<ServiceRequest>
            {
                ServiceRequest.Create(Guid.NewGuid(),
                    "WQX",
                    "Please turn off the bathroom light",
                    CurrentStatus.Created,
                    "John Smith",
                    DateTime.Now.AddDays(-1),
                    string.Empty,
                    null).Instance,
                ServiceRequest.Create(Guid.NewGuid(),
                    "SZX",
                    "Please turn off the bathroom light",
                    CurrentStatus.Created,
                    "Stan Lee",
                    DateTime.Now.AddDays(-1),
                    "John Foos",
                    DateTime.Now).Instance
            };
        }
    }
}