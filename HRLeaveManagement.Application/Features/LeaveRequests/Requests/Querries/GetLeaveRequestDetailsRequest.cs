using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries
{
    public class GetLeaveRequestDetailsRequest : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
