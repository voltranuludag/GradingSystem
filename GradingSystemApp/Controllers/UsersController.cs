using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;

namespace GradingSystemApp.Controllers
{
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getusersbyoperationclaimid")]
        public IActionResult GetUserByOperationClaimId(int operationClaimId)
        {
            var result = _userService.GetUsersByOperationClaimId(operationClaimId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
