using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class CreateLeaveRequestCommand : IRequest<int>
    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; }
    }
}
