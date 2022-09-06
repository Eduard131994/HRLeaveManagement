using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Querries;
using HRLeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationListRequestHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.Get(request.Id);

            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);

        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        }
    }
}
