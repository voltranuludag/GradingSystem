using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace GradingSystemApp.Controllers
{
    public class HomeworksController : Controller
    {
        private IHomeworkService _homeworkService;

        public HomeworksController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        [HttpGet("getallhomework")]
        public IActionResult GetAllHomework()
        {
            var result = _homeworkService.GetAllHomework();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addHomework")]
        public IActionResult AddHomework(Homework homework)
        {
            var result = _homeworkService.AddHomework(homework);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deletehomework")]
        public IActionResult DeleteHomework(Homework homework)
        {
            var result = _homeworkService.DeleteHomework(homework);
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
