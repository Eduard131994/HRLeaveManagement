using FluentValidation;

namespace HRLeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidator : AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(x => x.DefaultDays)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0)
                .LessThan(100);

        }
    }
}
