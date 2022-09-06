using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries;
using HRLeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Querries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestListRequestHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            return _mapper.Map<List<LeaveRequestListDto>>(leaveRequest);
        }
    }
}
