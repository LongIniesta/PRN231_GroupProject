using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Repositories;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IAccountRepository accountRepository;
        public AccountController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            accountRepository = new AccountRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {

        }

        [HttpGet]
        public IActionResult GetOne(int id) 
        {

        }


    }
}
