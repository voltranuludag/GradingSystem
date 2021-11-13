using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager:IOperationClaimSevice
    {
        private IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [CacheAspect]
        public IDataResult<IList<OperationClaim>> GetAllOperationClaim()
        {
            try
            {
                IList<OperationClaim> getListOperationClaim = _operationClaimDal.GetAll();
                return new SuccessDataResult<IList<OperationClaim>>(getListOperationClaim);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError, exception);
            }
        }

        [ValidationAspect(typeof(OperationClaimValidation))]
        [CacheRemoveAspect("IOperationClaimSevice.Get")]
        public IResult AddOperationClaim(OperationClaim operationClaim)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyExistName(operationClaim));
                if (result != null)
                {
                    return result;
                }
                _operationClaimDal.Add(operationClaim);
                return new Result(true, Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError, exception);
            }
        }

        private IResult AlreadyExistName(OperationClaim operationClaim)
        {
            bool result = _operationClaimDal.GetAll(x => x.Name== operationClaim.Name).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyPropertyName);
            }
            return new SuccessResult();
        }
    }
}
