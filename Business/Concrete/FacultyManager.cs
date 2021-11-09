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
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class FacultyManager:IFacultyService
    {
        private IFacultyDal _facultyDal;

        public FacultyManager(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        [CacheAspect]
        public IDataResult<IList<Faculty>> GetAllFaculty()
        {
            try
            {
                IList<Faculty> getListFaculty = _facultyDal.GetAll();
                return new SuccessDataResult<IList<Faculty>>(getListFaculty);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError,exception);
            }
            
        }

        [CacheRemoveAspect("IFacultyService.Get")]
        public IDataResult<Faculty> GetByFacultyId(int facultyId)
        {
            try
            {
                Faculty getFaculty = _facultyDal.Get(x => x.Id == facultyId);
                return new SuccessDataResult<Faculty>(getFaculty);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.Error,exception);
            }
        }

        [ValidationAspect(typeof(FacultyValidator))]
        [CacheRemoveAspect("IFacultyService.Get")]
        public IResult UpdateFaculty(Faculty faculty)
        {
            try
            {
                _facultyDal.Update(faculty);
                return new Result(true,Messages.Update);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.UpdateError,exception);
            }
        }

        [ValidationAspect(typeof(FacultyValidator))]
        [CacheRemoveAspect("IFacultyService.Get")]
        public IResult AddFacullty(Faculty faculty)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyExistName(faculty));
                if (result != null)
                {
                    return result;
                }
                _facultyDal.Add(faculty);
                return new Result(true,Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError,exception);
            }
        }

        [CacheRemoveAspect("IFacultyService.Get")]
        public IResult DeleteFaculty(Faculty faculty)
        {
            try
            {
                Faculty deleteFaculty = _facultyDal.Get(f => f.Id == faculty.Id);
                _facultyDal.Delete(deleteFaculty);
                return new Result(true,Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.DeletedError, exception);
            }
        }

        private IResult AlreadyExistName(Faculty faculty)
        {
            bool result = _facultyDal.GetAll(x => x.FacultyName == faculty.FacultyName).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyPropertyName);
            }
            return new SuccessResult();
        }
    }
}
