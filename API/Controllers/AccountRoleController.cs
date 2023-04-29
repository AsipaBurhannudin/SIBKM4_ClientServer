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
    public class AccountRoleController : GeneralController<IAccountRoleRepository, AccountRole, int>
    {
        public AccountRoleController(IAccountRoleRepository repository) : base(repository)
        {
        }
    }
}
