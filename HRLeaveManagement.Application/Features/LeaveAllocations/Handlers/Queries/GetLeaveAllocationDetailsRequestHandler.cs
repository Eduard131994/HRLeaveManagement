using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Querries;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailsRequestHandler : IRequestHandler<GetLeaveAllocationDetailsRequest, LeaveAllocationDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetLeaveAllocationDetailsRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);

        }
    }
}
