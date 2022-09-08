using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Persistence.Contracts;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeaveManagementDbContext _context;
        private ILeaveAllocationRepository _leaveAllocationRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private ILeaveTypeRepository _leaveTypeRepository;

        public UnitOfWork(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);

        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);

        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
