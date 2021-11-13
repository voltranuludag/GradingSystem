using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    public class FacultiesController : Controller
    {
        private IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet("getallfaculties")]
        public IActionResult GetAllFaculties()
        {
            var result = _facultyService.GetAllFaculty();
            if (result.Success)
            {
                return Ok(result);
            }
          
            return BadRequest(result);
        }

        [HttpGet("getbyfacultyid")]
        public IActionResult GetByFacultyId(int facultyId)
        {
            var result = _facultyService.GetByFacultyId(facultyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("updatefaculty")]
        public IActionResult UpdateFaculty(Faculty faculty)
        {
            var result = _facultyService.UpdateFaculty(faculty);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addfaculty")]
        public IActionResult AddFaculty(Faculty faculty)
        {
            var result = _facultyService.AddFacullty(faculty);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deletefaculty")]
        public IActionResult DeleteFaculty(Faculty faculty)
        {
            var result = _facultyService.DeleteFaculty(faculty);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
