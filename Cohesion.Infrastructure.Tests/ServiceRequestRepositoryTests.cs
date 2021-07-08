using Cohesion.Application.ServiceRequests;
using Cohesion.Domain.ServiceRequests;
using Cohesion.Infrastructure.InMemoryDataAccess;
using Cohesion.Infrastructure.InMemoryDataAccess.ServiceRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rhino.Mocks;
using System;
using System.Threading.Tasks;

namespace Cohesion.Infrastructure.Tests
{
    [TestClass]
    public class ServiceRequestRepositoryTests
    {
        private MockRepository _mockRepository;

        private DBContext _dBContext;
        private IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
            services.AddSingleton<DBContext>();

            var serviceProvider = services.BuildServiceProvider();

            _dBContext = serviceProvider.GetService<DBContext>();
            _serviceRequestRepository = serviceProvider.GetService<IServiceRequestRepository>();
            _mockRepository = new MockRepository();
        }

        [TestMethod]
        public async Task GetAll_Test()
        {
            var serviceRequestList = await _serviceRequestRepository.GetAll();
            Assert.IsTrue(serviceRequestList != null && serviceRequestList.Count > 0);
        }

        [TestMethod]
        public async Task GetById_OkTest()
        {
            var serviceRequestById = await _serviceRequestRepository.GetById(_dBContext.serviceRequests[1].Id);
            Assert.IsTrue(serviceRequestById != null && serviceRequestById.Id == _dBContext.serviceRequests[1].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetById_FailTest()
        {
            var serviceRequestById = await _serviceRequestRepository.GetById(Guid.NewGuid());
            Assert.Fail();
        }

        [TestMethod]
        public async Task PostServiceRequest_OkTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var jsonstring = @"{""Id"":""695FAD07-B3B4-44A2-89E1-E651C352E03B"",
                            ""buildingCode"": ""QX"",
                            ""description"": ""Please turn off the bathroom light"",
                            ""currentStatus"": 1,
                            ""createdBy"": ""Jhon Smith"",
                            ""createdDate"": ""2021-07-02T13:23:37.5384847-03:00"",
                            ""lastModifiedBy"": """",
                            ""lastModifiedDate"": null}";
            var objectfromjson = JsonConvert.DeserializeObject<ServiceRequest>(jsonstring);
            await _serviceRequestRepository.PostServiceRequest(objectfromjson);
            Assert.IsTrue(_dBContext.serviceRequests.Count > dbContextCount);
        }

        [TestMethod]
        
        public async Task PostServiceRequest_FailTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var serviceRequestObject = _dBContext.serviceRequests[0];
            await _serviceRequestRepository.PostServiceRequest(serviceRequestObject);
            Assert.IsTrue(_dBContext.serviceRequests.Count == dbContextCount);
        }

        [TestMethod]
        public async Task PutServiceRequest_OkTest()
        {
            var serviceRequestObject = _dBContext.serviceRequests[0];
            serviceRequestObject.Description = "test descrip";
            await _serviceRequestRepository.PutServiceRequestById(serviceRequestObject);
            Assert.IsTrue(_dBContext.serviceRequests[0].Description == "test descrip");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task PutServiceRequest_FailTest()
        {
            var serviceRequestObject = _dBContext.serviceRequests[0];
            _dBContext.serviceRequests.RemoveAll(x => x.Id == _dBContext.serviceRequests[0].Id);
            await _serviceRequestRepository.PutServiceRequestById(serviceRequestObject);
            Assert.Fail();
        }

        [TestMethod]
        public async Task DeleteById_OkTest()
        {
            var dbContextCount = _dBContext.serviceRequests.Count;
            var serviceRequestObject = _dBContext.serviceRequests[0];
            await _serviceRequestRepository.DeleteById(_dBContext.serviceRequests[0].Id);
            Assert.IsTrue(_dBContext.serviceRequests.Count <= dbContextCount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task DeleteById_FailTest()
        {
            await _serviceRequestRepository.DeleteById(Guid.NewGuid());
            Assert.Fail();
        }
    }
}
