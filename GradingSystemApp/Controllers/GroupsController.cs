using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace GradingSystemApp.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupService _groupService;

        [HttpGet("getallgroups")]
        public IActionResult GetAllGroups()
        {
            var result = _groupService.GetAllGroup();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addgroup")]
        public IActionResult AddGroup(Group group)
        {
            var result = _groupService.AddGroup(group);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deletegroup")]
        public IActionResult DeleteGroup(Group group)
        {
            var result = _groupService.DeleteGroup(group);
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
