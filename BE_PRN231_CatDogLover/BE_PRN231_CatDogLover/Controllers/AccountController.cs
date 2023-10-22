using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Repositories;
using DTOs.Account;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;
        private IAccountRepository _accountRepository;
        public AccountController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this._mapper = mapper;
            _accountRepository = new AccountRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] AccountSearchRequest searchRequest)
        {
            try
            {
                var response = _mapper.ProjectTo<AccountDTO>(_accountRepository.Search(searchRequest));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            try
            {
                return Ok(_mapper.Map<AccountDTO>(_accountRepository.GetById(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
/*
        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateAsync([FromForm] AccountUpdateProfileRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateAccount = _mapper.Map<Account>(updateRequest);
                 _accountRepository.UpdateAccount(updateAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }*/
    }
}
