using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<IList<UserDetailSelectedByOperationDto>> GetUsersByOperationClaimId(int operationClaimId);
        void Add(User user);
        User GetByMail(string email);
    }
}
