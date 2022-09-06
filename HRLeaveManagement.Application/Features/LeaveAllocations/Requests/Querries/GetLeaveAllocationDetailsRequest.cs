using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Querries
{
    public class GetLeaveAllocationDetailsRequest : IRequest<LeaveAllocationDto>
    {
        public int Id { get; set; }
    }
}
