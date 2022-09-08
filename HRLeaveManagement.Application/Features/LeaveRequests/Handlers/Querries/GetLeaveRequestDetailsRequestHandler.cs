using AutoMapper;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Querries
{
    public class GetLeaveRequestDetailsRequestHandler : IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public GetLeaveRequestDetailsRequestHandler(
            IMapper mapper,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}
