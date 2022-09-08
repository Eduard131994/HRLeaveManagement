using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Profiles;
using HRLeaveManagement.Application.Responses;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HRLeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockUnitOfWork = MockIUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _leaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test Dto"
            };
        }

        [Fact]
        public async Task CreateLeaveType()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockUnitOfWork.Object);
            var result = await handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockUnitOfWork.Object.LeaveTypeRepository.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();
            leaveTypes.Count.ShouldBeGreaterThan(2);
        }


        [Fact]
        public async Task CreateLeaveType_Invalid()
        {
            var leaveTypesBefore = await _mockUnitOfWork.Object.LeaveTypeRepository.GetAll();
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockUnitOfWork.Object);
            _leaveTypeDto.DefaultDays = -1;

            var result = await handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var leaveTypesAfter = await _mockUnitOfWork.Object.LeaveTypeRepository.GetAll();

            leaveTypesBefore.Count.ShouldBe(leaveTypesAfter.Count);
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.Count.ShouldBeGreaterThan(0);
        }

    }
}
