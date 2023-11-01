using BusinessObjects;
using DTOs.Account;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using AutoMapper;
using Repositories.Interface;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using DTOs.Report;
using DTOs.Pagination;
using System.Security.Principal;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;
        private IReportRepository _reportRepository;
        public ReportsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            _mapper = mapper;
            _reportRepository = new ReportRepository();
        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpGet]
        public IActionResult Search([FromQuery] ReportSearchRequest searchRequest)
        {
            try
            {
                var query = _reportRepository.Search(searchRequest);
                var response = _mapper.Map<List<ReportResponse>>(query.Data);
                return Ok(new PagedList<ReportResponse>(response, query.TotalResult, query.CurrentPage, query.Size));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpGet("{reporterId}/{reportedPersonId}")]
        public async Task<IActionResult> GetById(int reporterId, int reportedPersonId)
        {
            try
            {
                var query = await _reportRepository.GetReportById(reporterId, reportedPersonId);
                if (query == null) return NotFound();
                var response = _mapper.Map<ReportResponse>(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPost("CreateReport")]
        public async Task<ActionResult<ReportResponse>> Create(ReportRequest request)
        {
            try
            {
                await _reportRepository.AddReport(new Report()
                {
                    ReportedPersonId = request.ReportedPersonId,
                    ReporterId = request.ReporterId,
                    Content = request.Content,
                });
                return Created("", request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpPut("UpdateReport")]
        public async Task<IActionResult> Update(ReportRequest updateRequest)
        {
            try
            {
                await _reportRepository.UpdateReport(new Report()
                {
                    ReporterId = updateRequest.ReporterId,
                    ReportedPersonId=updateRequest.ReportedPersonId,
                    Content = updateRequest.Content,
                });
                return Ok(updateRequest);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Delete(int reporterId, int reportedPersonId)
        {
            try
            {
                await _reportRepository.RemoveReport(reporterId, reportedPersonId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
