using AutoMapper;
using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HRLeaveManagement.Application.Models;
using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public readonly IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequestValidator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = leaveRequestValidator.Validate(request.CreateLeaveRequestDto);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

            var email = new Email
            {
                To = "me@de.com",
                Body = "Leave Request Submitted",
                Subject = @$"Your leave request for {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D}
                    has been submitted successfully."
            };

            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception)
            {
                //log smth
            }
            return leaveRequest.Id;
        }
    }
}
