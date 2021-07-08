using Cohesion.Application.ServiceRequests;
using Cohesion.Domain.ServiceRequests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<ServiceRequest>>> Get()
        {
            try
            {
                List<ServiceRequest> serviceRequestList = await _serviceRequestAppService.GetServiceRequests();

                if (serviceRequestList.Count > 0) return Ok(serviceRequestList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpGet("{id:guid}", Name = "CreatedServiceRequest")]
        public async Task<ActionResult<ServiceRequest>> Get(Guid id)
        {
            try
            {
               return Ok(await _serviceRequestAppService.GetServiceRequestById(id));
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

        public async Task<ActionResult> Post([FromBody] ServiceRequestDto input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                ServiceRequestResult result = await _serviceRequestAppService.CreateServiceRequest(input);
                if (result.ErrorMessages.Count > 0) return BadRequest(result.ErrorMessages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(201);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ServiceRequestDto input)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                ServiceRequestResult result = await _serviceRequestAppService.EditServiceRequest(id, input);
                if (result.ErrorMessages.Count > 0) return BadRequest(result.ErrorMessages);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _serviceRequestAppService.DeleteServiceRequestById(id);
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