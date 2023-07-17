using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeeklyViewService.Model;
using WeeklyViewService.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeeklyViewService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class WeeklyViewController : ControllerBase
  {
    private readonly IWeeklyViewService _weeklyViewService;

    public WeeklyViewController(IWeeklyViewService weeklyViewService)
    {
      _weeklyViewService = weeklyViewService;
    }

    // GET: api/<WeeklyViewController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<WeeklyViewController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }


    // POST api/<WeeklyViewController>/details
    [HttpPost("details")]
    public async Task<ActionResult<IEnumerable<DailyDetail>>> GetWeeklyDetails([FromBody]WeeklyViewRequestModel request)
    {
      if (request == null) return BadRequest();
      var results = await _weeklyViewService.GetWeeklyDetails(request);
      return results;
    }

    // PUT api/<WeeklyViewController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<WeeklyViewController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
