using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class GroupHomeworkManager
    {
        private IGroupHomeworkDal _groupHomeworkDal;

        public GroupHomeworkManager(IGroupHomeworkDal groupHomeworkDal)
        {
            _groupHomeworkDal = groupHomeworkDal;
        }


        public IDataResult<IList<GroupHomework>> GetAllGroupHomework()
        {
            try
            {
                IList<GroupHomework> getList = _groupHomeworkDal.GetAll();
                return new SuccessDataResult<IList<GroupHomework>>(getList);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError, exception);
            }
        }

        [ValidationAspect(typeof(GroupHomeworkvalidator))]
        public IResult AddGroupHomework(GroupHomework grouphomework)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyTakeHomework(grouphomework));
                if (result != null)
                {
                    return result;
                }
                _groupHomeworkDal.Add(grouphomework);
                return new Result(true, Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError, exception);
            }
        }

        public IResult DeleteGroupHomework(GroupHomework grouphomework)
        {
            try
            {
                GroupHomework delete = _groupHomeworkDal.Get(f => f.Id == grouphomework.Id);
                _groupHomeworkDal.Delete(delete);
                return new Result(true, Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.DeletedError, exception);
            }
        }

        private IResult AlreadyTakeHomework(GroupHomework groupHomework)
        {
            bool result = _groupHomeworkDal.GetAll(x => (x.HomeworkId == groupHomework.HomeworkId) 
                                                        && (x.GroupId==groupHomework.GroupId) 
                                                        && (x.UserId==groupHomework.UserId)).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyTakeHomework);
            }
            return new SuccessResult();
        }
    }
}
