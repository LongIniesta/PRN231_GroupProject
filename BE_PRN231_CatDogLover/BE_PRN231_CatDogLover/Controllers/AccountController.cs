using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Repositories;
using DTOs.Account;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using System.Security.Claims;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;
        private IAccountRepository _accountRepository;
        public AccountController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            _mapper = mapper;
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

        /*  [AllowAnonymous]
          [HttpGet]
          [EnableQuery]
          public ActionResult<List<AccountDTO>> GetAccounts()
          {
              try
              {
                  List<Account> result = _accountRepository.GetAll().ToList();
                  return _mapper.Map<List<AccountDTO>>(result);
              }
              catch (Exception ex)
              {
                  return new BadRequestObjectResult(ex.Message);
              }
          }*/

        [AllowAnonymous]
        [HttpDelete("Ban")]
        public async Task<ActionResult<string>> Ban([FromQuery] int id, string reason)
        {
            try
            {
                await _accountRepository.BanAccountAsync(id, reason);

                return "Banned!";
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpPut("Unban")]
        public async Task<ActionResult<string>> Unban([FromQuery] int id)
        {
            try
            {
                await _accountRepository.UnbanAccountAsync(id);

                return "Unbanned!";
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("UpdateProfile")]
        public async Task<ActionResult<AccountDTO>> Update([FromForm] AccountUpdateProfileRequest updateRequest)
        {
            try
            {
                var acc = await _accountRepository.GetAccountById(updateRequest.AccountId);

                acc = _mapper.Map(updateRequest, acc);

                var result = await _accountRepository.UpdateAccount(acc);

                return _mapper.Map<AccountDTO>(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(AccountResetPassword request)
        {
            try
            {
                await _accountRepository.ResetPasswordAsync(request.AccountId, request.CurrentPassword, request.NewPassword);

                return "Reset Password successfully!";
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword(int id)
        {
            try
            {
                await _accountRepository.ForgetPasswordAsync(id);

                return "A mail with your password has been sent! Please check it out!";
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
