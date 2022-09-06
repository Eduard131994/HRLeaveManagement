using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Querries
{
    public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
    {
    }
}
