using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.Id);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }
    }
}
