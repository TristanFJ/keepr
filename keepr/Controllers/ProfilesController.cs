using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(AccountService accountService, KeepsService ks, VaultsService vs)
    {
      _accountService = accountService;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      return Ok(_accountService.GetById(id));
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetKeepsByProfile(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }



    }
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfile(string id)
    {
      try
      {
        List<Vault> vaults = _vs.GetVaultsByProfile(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}