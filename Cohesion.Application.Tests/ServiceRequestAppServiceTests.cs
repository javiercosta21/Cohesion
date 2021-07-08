using Cohesion.Application.ServiceRequests;
using Cohesion.Application.Utils;
using Cohesion.Infrastructure.InMemoryDataAccess;
using Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rhino.Mocks;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cohesion.Application.Tests
{
    [TestClass]
    public class ServiceRequestAppServiceTests
    {
        private MockRepository _mockRepository;

        private DBContext _dBContext;
        private IServiceRequestRepository _serviceRequestRepository;
        private IServiceRequestAppService _serviceRequestAppService;
        private IEmailSender _emailSender;
        public ServiceRequestAppServiceTests() 
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build());
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IServiceRequestAppService, ServiceRequestAppService>();
            services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddSingleton<DBContext>();

            var serviceProvider = services.BuildServiceProvider();

            _dBContext = serviceProvider.GetService<DBContext>();
            _emailSender = serviceProvider.GetService<IEmailSender>();
            _serviceRequestRepository = serviceProvider.GetService<IServiceRequestRepository>();
            _serviceRequestAppService = serviceProvider.GetService<IServiceRequestAppService>();
            
            _mockRepository = new MockRepository();
        }


        [TestMethod]
        public async Task GetServiceRequests_OKTest()
        {
            var serviceRequestList = await _serviceRequestAppService.GetServiceRequests();
            Assert.IsTrue(serviceRequestList != null && serviceRequestList.Count > 0);
        }

        [TestMethod]
        public async Task GetServiceRequests_FailTest()
        {
            _dBContext.serviceRequests = null;
            var serviceRequestList = await _serviceRequestAppService.GetServiceRequests();
            Assert.IsTrue(serviceRequestList == null);
        }

        [TestMethod]
        public async Task GetServiceRequestById_OKTest()
        {
            var guidId = _dBContext.serviceRequests[1].Id;
            var serviceRequestList = await _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.IsTrue(serviceRequestList != null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetServiceRequestById_FailTest()
        {
            var guidId = Guid.NewGuid();
            var serviceRequestList = await _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.Fail();
        }

        [TestMethod]
        public async Task CreateServiceRequest_OKTest()
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
            var serviceRequestObject = await _serviceRequestAppService.CreateServiceRequest(objectfromjson);
            Assert.IsTrue(serviceRequestObject != null && _dBContext.serviceRequests.Count > dbContextCount);
        }

        [TestMethod]
        public async Task CreateServiceRequest_FailTest()
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
            var serviceRequestObject = await _serviceRequestAppService.CreateServiceRequest(objectfromjson);
            Assert.IsTrue(serviceRequestObject.ErrorMessages.Count > 0 && _dBContext.serviceRequests.Count == dbContextCount);
        }

        [TestMethod]
        public async Task EditServiceRequest_OKTest()
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
            var serviceRequestObject = await _serviceRequestAppService.EditServiceRequest(originalId, objectfromjson);
            Assert.IsTrue(serviceRequestObject.Instance != null && serviceRequestObject.Instance.BuildingCode == "FFF" && _dBContext.serviceRequests.Count == dbContextCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task EditServiceRequest_FailTest()
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
            var serviceRequestObject = await _serviceRequestAppService.EditServiceRequest(originalId, objectfromjson);
            Assert.Fail();
        }

        [TestMethod]
        public async Task DeleteServiceRequestById_OKTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var guidId = _dBContext.serviceRequests[1].Id;
            await _serviceRequestAppService.DeleteServiceRequestById(guidId);
            Assert.IsTrue(_dBContext.serviceRequests.Count < dbContextCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DeleteServiceRequestById_FailTest()
        {
            var guidId = Guid.NewGuid();
            var serviceRequestList = await _serviceRequestAppService.GetServiceRequestById(guidId);
            Assert.Fail();
        }
    }
}
