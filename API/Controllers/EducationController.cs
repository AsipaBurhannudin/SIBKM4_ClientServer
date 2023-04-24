using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            // Handle ketika data tidak ada / kosong

            return Ok(new ResponseDataVM<IEnumerable<Education>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = educations
            });
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var educations = _educationRepository.GetById(id);
            if (educations == null)
            {
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Id Not Found"
                });
            }
            return Ok(new ResponseDataVM<Education>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = educations
            });
        }
        [HttpPost]
        public ActionResult Insert(Education education)
        {
            if (education.Major == "" || education.Major.ToLower() == "string")
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }
            var insert = _educationRepository.Insert(education);
            if (insert > 0)
                return Ok(new ResponseDataVM<Education>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Insert Success",
                    Data = null
                });
            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Errors = "Insert Failed / Lost Connection"
            });
        }
        [HttpPut]
        public ActionResult Update(Education education)
        {
            if (education.Major == "" || education.Major.ToLower() == "string")
            {
                return BadRequest(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Errors = "Value Cannot be Null or Default"
                });
            }

            var update = _educationRepository.Update(education);
            if (update > 0) return Ok(new ResponseDataVM<Education>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Success",
                Data = null
            });

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Errors = "Update Failed / Lost Connection"
            });
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var delete = _educationRepository.Delete(id);
            if (delete > 0)
                return Ok(new ResponseDataVM<Education>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Delete Success",
                    Data = null
                });

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Delete Failed / Lost Connection"
            });
        }
    }
}
