using API.Base;
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
    public class EducationController : GeneralController<IEducationRepository, Education, int>
    {
        public EducationController(IEducationRepository repository) : base(repository)
        {
        }
    }
}
