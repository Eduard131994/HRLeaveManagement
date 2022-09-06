using HRLeaveManagement.Application.Persistence.Contracts;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
