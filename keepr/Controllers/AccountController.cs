using System;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace keepr.Controllers
{
  [ApiController]
  [Authorize]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vs;

    public AccountController(AccountService accountService, VaultsService vs)
    {
      _accountService = accountService;
      _vs = vs;
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

    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetMyVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.GetMyVaults(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }


}