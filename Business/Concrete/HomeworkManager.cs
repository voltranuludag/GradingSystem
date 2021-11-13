using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class HomeworkManager:IHomeworkService
    {
        private IHomeworkDal _homeworkDal;

        public HomeworkManager(IHomeworkDal homeworkDal)
        {
            _homeworkDal = homeworkDal;
        }

        public IDataResult<IList<Homework>> GetAllHomework()
        {
            try
            {
                IList<Homework> getList = _homeworkDal.GetAll();
                return new SuccessDataResult<IList<Homework>>(getList);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError, exception);
            }
        }

        [ValidationAspect(typeof(Homeworkvalidator))]
        public IResult AddHomework(Homework homework)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyExistName(homework));
                if (result != null)
                {
                    return result;
                }
                _homeworkDal.Add(homework);
                return new Result(true, Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError, exception);
            }
        }
        
        public IResult DeleteHomework(Homework homework)
        {
            try
            {
                Homework delete = _homeworkDal.Get(f => f.Id == homework.Id);
                _homeworkDal.Delete(delete);
                return new Result(true, Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.DeletedError, exception);
            }
        }

        private IResult AlreadyExistName(Homework homework)
        {
            bool result = _homeworkDal.GetAll(x => x.HomeworkName == homework.HomeworkName).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyPropertyName);
            }
            return new SuccessResult();
        }
    }
}
