using System;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Authorize]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpGet("vaults")]
    // public async Task<ActionResult<List<Vault>>> GetVaults()
    // {
    //     try
    //     {

    //     }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

  }


}