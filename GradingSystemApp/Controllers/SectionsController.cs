using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    public class SectionsController : Controller
    {
        private ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getallsections")]
        public IActionResult GetAllSections()
        {
            IDataResult<IList<Section>> result = _sectionService.GetAllSection();
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getsectionsbyfacultyid")]
        public IActionResult GetSectionsByFacultyId(int facultyId)
        {
            IDataResult<IList<Section>> result = _sectionService.GetSectionsByFaculltyId(facultyId);
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbysectionid")]
        public IActionResult GetBySectionId(int sectionId)
        {
            var result = _sectionService.GetBySectionId(sectionId);
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }

        [HttpPost("updatesection")]
        public IActionResult UpdateSection(Section section)
        {
            var result = _sectionService.UpdateSection(section);
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addsection")]
        public IActionResult AddSection(Section section)
        {
            var result = _sectionService.AddSection(section);
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteSection")]
        public IActionResult DeleteSection(Section section)
        {
            var result = _sectionService.DeleteSection(section);
            if (result.Success)
            {
                return View(result);
            }

            return BadRequest(result);
        }
    }
}
