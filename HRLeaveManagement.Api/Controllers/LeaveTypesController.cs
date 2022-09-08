using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HRLeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _meadiator;

        public LeaveTypesController(IMediator meadiator)
        {
            _meadiator = meadiator;
        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _meadiator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType = await _meadiator.Send(new GetLeaveTypeDetailRequest() { Id = id });
            return Ok(leaveType);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            var command = new CreateLeaveTypeCommand { LeaveTypeDto = leaveType };
            var response = await _meadiator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveType)
        {
            var command = new UpdateLeaveTypeCommand { LeaveTypeDto = leaveType };
            await _meadiator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            await _meadiator.Send(command);
            return NoContent();
        }
    }
}
