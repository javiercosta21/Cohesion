using Cohesion.Domain;
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

                //new ServiceRequest{ BuildingCode = "WQX", createdBy = "John Smith", createdDate = DateTime.Now.AddDays(-1)
                //                , currentStatus = CurrentStatus.Created, description = "Please turn off the bathroom light"
                //                , lastModifiedBy = "", lastModifiedDate = null},
                //new ServiceRequest{ buildingCode ="SZX", createdBy = "Stan Lee", createdDate = DateTime.Now.AddDays(-1)
                //                , currentStatus = CurrentStatus.InProgress, description = "Please close all windows"
                //                , lastModifiedBy = "John Foos", lastModifiedDate = DateTime.Now},
                //new ServiceRequest{ buildingCode ="WQX", createdBy = "Peter Jacobsen", createdDate = DateTime.Now.AddDays(-1)
                //                , currentStatus = CurrentStatus.Canceled, description = "Please turn on tv"
                //                , lastModifiedBy = "Laura Taylor", lastModifiedDate = DateTime.Now},
                //new ServiceRequest{ buildingCode ="RTW", createdBy = "Josh Hernandez", createdDate = DateTime.Now.AddDays(-1)
                //                , currentStatus = CurrentStatus.Complete, description = "Please set room temperature to 20 degrees"
                //                , lastModifiedBy = "Sarah Jones", lastModifiedDate = DateTime.Now}
            };
        }
    }
}