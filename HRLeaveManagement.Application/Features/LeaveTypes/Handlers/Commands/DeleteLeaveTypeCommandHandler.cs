using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.Id);
            if (leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            await _unitOfWork.LeaveTypeRepository.Delete(leaveType);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
