using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;

namespace GradingSystemApp.Controllers
{
    public class OperationClaimsController : Controller
    {
        private IOperationClaimSevice _operationClaimSevice;

        public OperationClaimsController(IOperationClaimSevice operationClaimSevice)
        {
            _operationClaimSevice = operationClaimSevice;
        }

        [HttpGet("getalloperationclaims")]
        public IActionResult GetAllFaculties()
        {
            var result = _operationClaimSevice.GetAllOperationClaim();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addoperationclaim")]
        public IActionResult AddFaculty(OperationClaim operationClaim)
        {
            var result = _operationClaimSevice.AddOperationClaim(operationClaim);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
