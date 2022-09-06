using FluentValidation;
using HRLeaveManagement.Application.Persistence.Contracts;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        public ILeaveAllocationDtoValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            RuleFor(x => x.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be grater than {ComparisonValue}");

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be grater after {ComparisonValue}");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveAllocation = await leaveAllocationRepository.Exists(id);
                    return !leaveAllocation;
                })
                .WithMessage("{PropertyName} does not exist.");

        }
    }
}
