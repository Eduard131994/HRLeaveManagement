using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries;
using HRLeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Querries
{
    public class GetLeaveRequestDetailsRequestHandler : IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestDetailsRequestHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }
    }
}
