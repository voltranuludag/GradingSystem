using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, GradingSystemDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new GradingSystemDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public IList<UserSelectedByOperationDto> GetUserSelectedByOperationId(int operationId)
        {
            using (var context = new GradingSystemDbContext())
            {
                var result = from user in context.Users
                    join userClaim in context.UserOperationClaims on user.Id equals userClaim.UserId
                    join operation in context.OperationClaims on userClaim.OperationClaimId equals operation.Id
                    where userClaim.Id == operationId
                    select new UserSelectedByOperationDto
                    {
                        UserId = user.Id,
                        UserOperationId = userClaim.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        OperationName = operation.Name,
                        Status = user.Status
                    };
                return result.ToList();
            }
        }
    }
}
