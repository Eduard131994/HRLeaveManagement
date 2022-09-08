using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries;
using HRLeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest() { IsLoggedInUser = isLoggedInUser });
            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestListDto>> Get(int id)
        {
            var leaveReqesut = await _mediator.Send(new GetLeaveRequestDetailsRequest() { Id = id });
            return Ok(leaveReqesut);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            var leaveReqesutId = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequestDto = createLeaveRequestDto });
            return Ok(leaveReqesutId);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { Id = id, LeaveRequestDto = updateLeaveRequestDto });
            return NoContent();
        }

        [HttpPut("/change/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApprovalDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
            return NoContent();
        }
    }
}
