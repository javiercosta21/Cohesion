using Cohesion.Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace Cohesion.Application.Tests
{
    [TestClass]
    public class EmailSenderTests
    {
        private readonly IEmailSender _emailSender;
   
        public EmailSenderTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build());
            var serviceProvider = services.BuildServiceProvider();

            _emailSender = serviceProvider.GetService<IEmailSender>();
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SendMail_FailTest()
        {
            _emailSender.SendMail("695FAD07-B3B4-44A2-89E1-E651C352E03B");
            Assert.Fail();
        }
    }
}
