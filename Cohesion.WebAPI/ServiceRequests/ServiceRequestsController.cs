using Cohesion.Application.ServiceRequests;
using Cohesion.Domain.ServiceRequests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Cohesion.WebAPI.ServiceRequests
{
    [Route("api/ServiceRequest")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestAppService _serviceRequestAppService;

        public ServiceRequestsController(IServiceRequestAppService serviceRequestAppService) =>
            _serviceRequestAppService = serviceRequestAppService;

        [HttpGet(Name = "DeletedServiceRequest")]
        public ActionResult<List<ServiceRequest>> Get()
        {
            try
            {
                List<ServiceRequest> serviceRequestList = _serviceRequestAppService.GetServiceRequests();

                if (serviceRequestList.Count > 0) return Ok(serviceRequestList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "CreatedServiceRequest")]
        public ActionResult<ServiceRequest> Get(Guid id)
        {
            try
            {
               return Ok(_serviceRequestAppService.GetServiceRequestById(id));
            }
            catch(InvalidOperationException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public ActionResult Post([FromBody] ServiceRequestDto input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                ServiceRequestResult result = _serviceRequestAppService.CreateServiceRequest(input);
                if (result.ErrorMessages.Count > 0) return BadRequest(result.ErrorMessages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(201);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, [FromBody] ServiceRequestDto input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                ServiceRequestResult result = _serviceRequestAppService.EditServiceRequest(id, input);
                if (result.ErrorMessages.Count > 0) return BadRequest(result.ErrorMessages);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _serviceRequestAppService.DeleteServiceRequestById(id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return StatusCode(201);
        }
    }
}