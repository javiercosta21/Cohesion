using Cohesion.Domain.ServiceRequests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cohesion.Domain.Tests
{
    [TestClass]
    public class ServiceRequestTests
    {
        [TestMethod]
        public void Create_OkTest()
        {
            var serviceRequestResult = ServiceRequest.Create(Guid.NewGuid(), "WWW", "test", CurrentStatus.Created, "john", DateTime.Now.AddDays(-1),
                "carl", DateTime.Now);
            Assert.IsTrue(serviceRequestResult.ErrorMessages.Count == 0 && serviceRequestResult.Instance.BuildingCode == "WWW");
        }

        [TestMethod]
        public void Create_FailTest()
        {
            var serviceRequestResult = ServiceRequest.Create(Guid.NewGuid(), "W", "test", CurrentStatus.Created, "john", DateTime.Now.AddDays(-1),
                "carl", DateTime.Now);
            Assert.IsTrue(serviceRequestResult.ErrorMessages.Count != 0);
        }

        [TestMethod]
        public void Edit_OkTest()
        { 
            var serviceRequestResult = ServiceRequest.Create(Guid.NewGuid(), "WWW", "test", CurrentStatus.Created, "john", DateTime.Now.AddDays(-1),
                "carl", DateTime.Now);
            Assert.IsTrue(serviceRequestResult.ErrorMessages.Count == 0 && serviceRequestResult.Instance.BuildingCode == "WWW");
        }

        [TestMethod]
        public void Edit_FailTest()
        {
            var serviceRequestResult = ServiceRequest.Create(Guid.NewGuid(), "W", "test", CurrentStatus.Created, "john", DateTime.Now.AddDays(-1),
                "carl", DateTime.Now);
            Assert.IsTrue(serviceRequestResult.ErrorMessages.Count != 0);
        }
    }
}
