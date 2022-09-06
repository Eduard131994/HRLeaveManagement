using FluentValidation;
using HRLeaveManagement.Application.Persistence.Contracts;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        public UpdateLeaveAllocationDtoValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            Include(new ILeaveAllocationDtoValidator(leaveAllocationRepository));
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}