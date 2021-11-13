using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class GroupManager : IGroupService
    {
        private IGroupDal _groupDal;
        public IDataResult<IList<Group>> GetAllGroup()
        {
            try
            {
                IList<Group> getList = _groupDal.GetAll();
                return new SuccessDataResult<IList<Group>>(getList);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError,exception);
            }
        }

        public IResult AddGroup(Group group)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyExistName(group));
                if (result != null)
                {
                    return result;
                }
                _groupDal.Add(group);
                return new Result(true, Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError, exception);
            }
        }

        public IResult DeleteGroup(Group group)
        {
            try
            {
                Group delete= _groupDal.Get(f => f.Id == group.Id);
                _groupDal.Delete(delete);
                return new Result(true, Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.DeletedError, exception);
            }
        }

        private IResult AlreadyExistName(Group group)
        {
            bool result = _groupDal.GetAll(x => x.GroupName== group.GroupName).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyPropertyName);
            }
            return new SuccessResult();
        }
    }
}
