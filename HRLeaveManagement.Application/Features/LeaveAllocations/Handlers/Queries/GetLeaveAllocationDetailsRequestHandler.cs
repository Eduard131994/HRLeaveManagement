using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Querries;
using HRLeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailsRequestHandler : IRequestHandler<GetLeaveAllocationDetailsRequest, LeaveAllocationDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailsRequestHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);

        }
    }
}
