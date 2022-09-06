using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Domain;
using Moq;
using System.Collections.Generic;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRpository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType() { Id = 1, Name = "Test Vacation", DefaultDays = 10},
                new LeaveType() { Id = 2, Name = "Test Sick", DefaultDays = 15},
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            return mockRepo;
        }
    }
}
