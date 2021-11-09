using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFacultyService
    {
        IDataResult<IList<Faculty>> GetAllFaculty();
        IDataResult<Faculty> GetByFacultyId(int facultyId);
        IResult UpdateFaculty(Faculty faculty);
        IResult AddFacullty(Faculty faculty);
        IResult DeleteFaculty(Faculty faculty);
    }
}
