using FluentValidation;
using HRLeaveManagement.Application.Persistence.Contracts;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(x => x.StartDate)
                .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(x => x.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await leaveTypeRepository.Exists(id);
                    return leaveTypeExists;
                }).WithMessage("{PropertyName} does not exist");
        }
    }
}
