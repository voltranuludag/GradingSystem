using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IHomeworkService
    {
        IDataResult<IList<Homework>> GetAllHomework();
        IResult AddHomework(Homework homework);
        IResult DeleteHomework(Homework homework);
    }
}