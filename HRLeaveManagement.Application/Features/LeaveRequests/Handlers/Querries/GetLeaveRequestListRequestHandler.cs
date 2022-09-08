using AutoMapper;
using HRLeaveManagement.Application.Constants;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Querries;
using HRLeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Querries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetLeaveRequestListRequestHandler(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;


        }

    }
}
