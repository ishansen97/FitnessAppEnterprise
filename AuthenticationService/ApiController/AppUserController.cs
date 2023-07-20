using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Model;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationService.ApiController
{
  [Route("api/[controller]")]
  [ApiController]
  //[Authorize]
  public class AppUserController : ControllerBase
  {
    private readonly IUserService _userService;

    public AppUserController(IUserService userService)
    {
      _userService = userService;
    }

    // GET: api/<AppUserController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<AppUserController>/{userId}
    [HttpGet("{userId}")]
    public async Task<ActionResult<AppUserModel>> Get(string userId)
    {
      var model = await _userService.GetUser(userId);
      if (model == null) return NotFound();
      return model;
    }

    // POST api/<AppUserController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<AppUserController>/update/{userId}
    [HttpPut("update/{userId}")]
    public async Task<ActionResult> Put(string userId, [FromBody] AppUserModel request)
    {
      if (request == null) return BadRequest();
      await _userService.UpdateUser(userId, request);
      return Ok();
    }

    // DELETE api/<AppUserController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
