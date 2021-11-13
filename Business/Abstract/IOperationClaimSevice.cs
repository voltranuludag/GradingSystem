﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOperationClaimSevice
    {
        IDataResult<IList<OperationClaim>> GetAllOperationClaim();
        IResult AddOperationClaim(OperationClaim operationClaim);
    }
}
