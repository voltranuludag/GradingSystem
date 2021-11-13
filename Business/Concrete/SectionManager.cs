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
    public class SectionManager:ISectionService
    {
        private ISectionDal _sectionDal;

        public SectionManager(ISectionDal sectionDal)
        {
            _sectionDal = sectionDal;
        }

        [CacheAspect]
        public IDataResult<IList<Section>> GetAllSection()
        {
            try
            {
                IList<Section> getListSections= _sectionDal.GetAll();
                return new SuccessDataResult<IList<Section>>(getListSections);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError, exception);
            }
        }

        public IDataResult<Section> GetBySectionId(int sectionId)
        {
            try
            {
                Section getSection = _sectionDal.Get(x=>x.Id==sectionId);
                return new SuccessDataResult<Section>(getSection);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.Error, exception);
            }
        }

        public IDataResult<IList<Section>> GetSectionsByFaculltyId(int facultyId)
        {
            try
            {
                IList<Section> getListSections = _sectionDal.GetAll(x=>x.FacultyId == facultyId);
                return new SuccessDataResult<IList<Section>>(getListSections);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.ListedError, exception);
            }
        }

        [ValidationAspect(typeof(SectionValidator))]
        [CacheRemoveAspect("ISectionService.Get")]
        public IResult UpdateSection(Section section)
        {
            try
            {
                _sectionDal.Update(section);
                return new Result(true, Messages.Update);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.UpdateError, exception);
            }
        }

        [ValidationAspect(typeof(SectionValidator))]
        [CacheRemoveAspect("ISectionService.Get")]
        public IResult AddSection(Section section)
        {
            try
            {
                IResult result = BusinessRules.Run(AlreadyExistName(section));
                if (result != null)
                {
                    return result;
                }
                _sectionDal.Add(section);
                return new Result(true, Messages.Added);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.AddError, exception);
            }
        }

        [CacheRemoveAspect("ISectionService.Get")]
        public IResult DeleteSection(Section section)
        {
            try
            {
                Section deleteSection = _sectionDal.Get(f => f.Id == section.Id);
                _sectionDal.Delete(deleteSection);
                return new Result(true, Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(Messages.DeletedError, exception);
            }
        }

        private IResult AlreadyExistName(Section section)
        {
            bool result = _sectionDal.GetAll(x => x.SectionName== section.SectionName).Any();
            if (result)
            {
                return new ErrorResult(Messages.AlreadyPropertyName);
            }
            return new SuccessResult();
        }

    }
}
