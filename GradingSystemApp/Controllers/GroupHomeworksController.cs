using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace GradingSystemApp.Controllers
{
    public class GroupHomeworksController : Controller
    {
        private IGroupHomeworkService _groupHomeworkService;

        [HttpGet("getallgrouphomework")]
        public IActionResult GetAllGroupHomework()
        {
            var result = _groupHomeworkService.GetAllGroupHomework();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addgrouphomework")]
        public IActionResult AddGroupHomework(GroupHomework groupHomework)
        {
            var result = _groupHomeworkService.AddGroupHomework(groupHomework);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deletegrouphomework")]
        public IActionResult DeleteGroupHomework(GroupHomework groupHomework)
        {
            var result = _groupHomeworkService.DeleteGroupHomework(groupHomework);
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
