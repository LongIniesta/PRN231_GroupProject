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
using Microsoft.AspNet.OData;
using DTOs.Pagination;

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
                var response = _mapper.ProjectTo<AccountDTO>(_accountRepository.SearchWithoutPagiantion(searchRequest));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 🌟NEW🌟 for search with pagination
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns>List of account</returns>
        [AllowAnonymous]
        [HttpGet("/new")]
        public async Task<IActionResult> Search(AccountSearchRequest searchRequest)
        {
            try
            {
                var notMappedResponse = await _accountRepository.Search(searchRequest);
                var response = _mapper.Map<List<AccountDTO>>(notMappedResponse.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var notMappedResponse = await _accountRepository.GetAccountById(id);
                var response = _mapper.Map<AccountDTO>(notMappedResponse);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create account for admin level
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<AccountDTO>> CreateAccount(AccountCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Data invalid");
            if (request.PasswordConfirm != request.Password)
            {
                return BadRequest("Password not match with confirm");
            }
            if (_accountRepository.GetAll().Any(a => a.Email.ToLower().Trim() == request.Email.ToLower().Trim()))
            {
                return BadRequest("Email are already exist");
            }
            Account account = new Account();
            try
            {
                account = _mapper.Map<Account>(request);
                account.CreateDate = DateTime.Now;
                account.Status = true;
                account.Version = 1;
                await _accountRepository.AddAccount(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("", _mapper.Map<AccountDTO>(account));
        }

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
        public async Task<ActionResult<AccountDTO>> Update(AccountUpdateProfileRequest updateRequest)
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
