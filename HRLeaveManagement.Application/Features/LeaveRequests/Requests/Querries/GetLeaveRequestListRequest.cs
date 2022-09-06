using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries
{
    public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
    {
    }
}
