using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HRLeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveallocationValidator = new UpdateLeaveAllocationDtoValidator(_leaveAllocationRepository);
            var validationResult = leaveallocationValidator.Validate(request.LeaveAllocationDto);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            await _leaveAllocationRepository.Update(leaveAllocation);

            return Unit.Value;
        }
    }
}
