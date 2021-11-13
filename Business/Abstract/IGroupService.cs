using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IGroupService
    {
        IDataResult<IList<Group>> GetAllGroup();
        IResult AddGroup(Group group);
        IResult DeleteGroup(Group group);
    }
}