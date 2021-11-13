using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISectionService
    {
        IDataResult<IList<Section>> GetAllSection();
        IDataResult<Section> GetBySectionId(int sectionId);
        IDataResult<IList<Section>> GetSectionsByFaculltyId(int facultyId);
        IResult UpdateSection(Section section);
        IResult AddSection(Section section);
        IResult DeleteSection(Section section);
    }
}
