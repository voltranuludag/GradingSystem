using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IGroupHomeworkService
    {
        IDataResult<IList<GroupHomework>> GetAllGroupHomework();
        IResult AddGroupHomework(GroupHomework groupHomework);
        IResult DeleteGroupHomework(GroupHomework groupHomework);
    }
}