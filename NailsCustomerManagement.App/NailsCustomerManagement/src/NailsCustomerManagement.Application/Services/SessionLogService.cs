using NailsCustomerManagement.Core.Entities;
using NailsCustomerManagement.Application.Interfaces.Application;
using NailsCustomerManagement.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsCustomerManagement.Application.Services
{
    public class SessionLogService : ISessionLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int InsertSessionLog(SysSessionLog session)
        {
            _unitOfWork.SessionLogRepo.InsertSessionLog(session);
            _unitOfWork.Complete();
            return session.SessionId;
        }

        public int UpdateSessionLog(SysSessionLog session)
        {
            _unitOfWork.SessionLogRepo.UpdateSessionLog(session);
            _unitOfWork.Complete(); 
            return session.SessionId;
        }
    }
}
