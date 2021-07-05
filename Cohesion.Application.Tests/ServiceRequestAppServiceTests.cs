using Cohesion.Application.ServiceRequests;
using Cohesion.Infrastructure.InMemoryDataAccess;
using Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rhino.Mocks;
using System;

namespace Cohesion.Application.Tests
{
    [TestClass]
    public class ServiceRequestAppServiceTests
    {
        private MockRepository _mockRepository;

        private DBContext _dBContext;
        private IServiceRequestRepository _serviceRequestRepository;
        private IServiceRequestAppService _serviceRequestAppService;

        public ServiceRequestAppServiceTests() 
        {
            var services = new ServiceCollection();
            services.AddTransient<IServiceRequestAppService, ServiceRequestAppService>();
            services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddSingleton<DBContext>();

            var serviceProvider = services.BuildServiceProvider();

            _dBContext = serviceProvider.GetService<DBContext>();
            _serviceRequestAppService = serviceProvider.GetService<IServiceRequestAppService>();
            _serviceRequestRepository = serviceProvider.GetService<IServiceRequestRepository>();
            _mockRepository = new MockRepository();
        }


        [TestMethod]
        public void GetServiceRequests_OKTest()
        {
            var serviceRequestList = _serviceRequestAppService.GetServiceRequests();
            Assert.IsTrue(serviceRequestList != null && serviceRequestList.Count > 0);
        }

        [TestMethod]
        public void GetServiceRequests_FailTest()
        {
            _dBContext.serviceRequests = null;
            var serviceRequestList = _serviceRequestAppService.GetServiceRequests();
            Assert.IsTrue(serviceRequestList == null);
        }

        [TestMethod]
        public void GetServiceRequestById_OKTest()
        {
            var guidId = _dBContext.serviceRequests[1].Id;
            var serviceRequestList = _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.IsTrue(serviceRequestList != null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetServiceRequestById_FailTest()
        {
            var guidId = Guid.NewGuid();
            var serviceRequestList = _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.Fail();
        }

        [TestMethod]
        public void CreateServiceRequest_OKTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var jsonstring = @"{
                            ""buildingCode"": ""WQX"",
                            ""description"": ""Please turn off the bathroom light"",
                            ""currentStatus"": 1,
                            ""createdBy"": ""Jhon Smith"",
                            ""createdDate"": ""2021-07-02T13:23:37.5384847-03:00"",
                            ""lastModifiedBy"": """",
                            ""lastModifiedDate"": null}";
            var objectfromjson = JsonConvert.DeserializeObject<ServiceRequestDto>(jsonstring);
            var serviceRequestObject = _serviceRequestAppService.CreateServiceRequest(objectfromjson);
            Assert.IsTrue(serviceRequestObject != null && _dBContext.serviceRequests.Count > dbContextCount);
        }

        [TestMethod]
        public void CreateServiceRequest_FailTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var jsonstring = @"{
                            ""buildingCode"": ""QX"",
                            ""description"": ""Please turn off the bathroom light"",
                            ""currentStatus"": 1,
                            ""createdBy"": ""Jhon Smith"",
                            ""createdDate"": ""2021-07-02T13:23:37.5384847-03:00"",
                            ""lastModifiedBy"": """",
                            ""lastModifiedDate"": null}";
            var objectfromjson = JsonConvert.DeserializeObject<ServiceRequestDto>(jsonstring);
            var serviceRequestObject = _serviceRequestAppService.CreateServiceRequest(objectfromjson);
            Assert.IsTrue(serviceRequestObject.ErrorMessages.Count > 0 && _dBContext.serviceRequests.Count == dbContextCount);
        }

        [TestMethod]
        public void EditServiceRequest_OKTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var originalId = _dBContext.serviceRequests[1].Id;
            var jsonstring = @"{
                            ""buildingCode"": ""FFF"",
                            ""description"": ""Please turn on the bathroom light"",
                            ""currentStatus"": 2,
                            ""createdBy"": ""Jhon Carl"",
                            ""createdDate"": ""2021-07-02T13:23:37.5384847-03:00"",
                            ""lastModifiedBy"": """",
                            ""lastModifiedDate"": null}";
            var objectfromjson = JsonConvert.DeserializeObject<ServiceRequestDto>(jsonstring);
            var serviceRequestObject = _serviceRequestAppService.EditServiceRequest(originalId, objectfromjson);
            Assert.IsTrue(serviceRequestObject.Instance != null && serviceRequestObject.Instance.BuildingCode == "FFF" && _dBContext.serviceRequests.Count == dbContextCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EditServiceRequest_FailTest()
        {
            var originalId = Guid.NewGuid();
            var jsonstring = @"{
                            ""buildingCode"": ""RQX"",
                            ""description"": ""Please turn off the bathroom light"",
                            ""currentStatus"": 1,
                            ""createdBy"": ""Jhon Smith"",
                            ""createdDate"": ""2021-07-02T13:23:37.5384847-03:00"",
                            ""lastModifiedBy"": """",
                            ""lastModifiedDate"": null}";
            var objectfromjson = JsonConvert.DeserializeObject<ServiceRequestDto>(jsonstring);
            var serviceRequestObject = _serviceRequestAppService.EditServiceRequest(originalId, objectfromjson);
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteServiceRequestById_OKTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var guidId = _dBContext.serviceRequests[1].Id;
            _serviceRequestAppService.DeleteServiceRequestById(guidId);
            Assert.IsTrue(_dBContext.serviceRequests.Count < dbContextCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteServiceRequestById_FailTest()
        {
            var guidId = Guid.NewGuid();
            var serviceRequestList = _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.Fail();
        }
    }
}
