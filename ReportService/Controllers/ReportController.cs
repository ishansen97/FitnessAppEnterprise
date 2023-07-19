using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ReportService.Model;
using ReportService.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ReportController : ControllerBase
  {
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
      _reportService = reportService;
    }

    // GET: api/<ReportController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<ReportController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<ReportController>
    [HttpPost("add/")]
    public async Task<ActionResult<ReportResponseModel>> Post([FromBody] CreateReportRequestModel request)
    {
      if (request == null) return BadRequest();
      var response = await _reportService.CreateReportAsync(request);
      return response;
    }

    // PUT api/<ReportController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ReportController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
