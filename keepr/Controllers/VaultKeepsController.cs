using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Authorize]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {

    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }


    // BUG my invalid-auth create vaultKeep test fails but I'm not sure why. I'm authorizing the whole controller. Delete vaultkeep throws bad request. 
    // Request Failed | AssertionError: The request should throw a 403 when not authenticated: expected 200 to be one of [ 400, 403 ]
    // But the request they're sending is authenticated, because even the creator is being populated, userInfo isn't null. 
    // Do I have to do a creator check against something? Because it's "INVALID AUTH" and not "NO AUTH", but I'm not sure what to check against on a post besides if you're logged in. 
    [HttpPost]
    public async Task<ActionResult<VaultKeepViewModel>> Create([FromBody] VaultKeepViewModel newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = userInfo.Id;
        VaultKeepViewModel vaultKeep = _vks.Create(newVaultKeep);
        vaultKeep.Creator = userInfo;
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<VaultKeepViewModel>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("DELETED");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}