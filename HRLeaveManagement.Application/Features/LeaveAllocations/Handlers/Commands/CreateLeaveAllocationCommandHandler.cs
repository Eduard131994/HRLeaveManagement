using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Application.Responses;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_leaveAllocationRepository);
            var validationResult = validator.Validate(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);

                leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
                response.Message = "Creation successful";
                response.Id = leaveAllocation.Id;
            }

            return response;
        }
    }
}
