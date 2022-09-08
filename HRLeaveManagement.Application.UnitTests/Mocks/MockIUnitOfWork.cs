using HRLeaveManagement.Application.Contracts.Persistence;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    public static class MockIUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.LeaveTypeRepository).Returns(MockLeaveTypeRepository.GetLeaveTypeRpository().Object);

            return mockRepo;
        }
    }
}
