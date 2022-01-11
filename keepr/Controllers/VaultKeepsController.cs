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
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {

    private readonly VaultKeepsService _vks;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    public VaultKeepsController(VaultKeepsService vks, KeepsService ks, VaultsService vs)
    {
      _vks = vks;
      _ks = ks;
      _vs = vs;
    }


    // BUG my invalid-auth create vaultKeep test fails but I'm not sure why. I'm authorizing the whole controller. Delete vaultkeep throws bad request. 
    // Request Failed | AssertionError: The request should throw a 403 when not authenticated: expected 200 to be one of [ 400, 403 ]
    // But the request they're sending is authenticated, because even the creator is being populated, userInfo isn't null. 
    // Do I have to do a creator check against something? Because it's "INVALID AUTH" and not "NO AUTH", but I'm not sure what to check against on a post besides if you're logged in. 
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeepViewModel>> Create([FromBody] VaultKeepViewModel newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = userInfo.Id;
        Vault vault = _vs.GetById(newVaultKeep.VaultId);
        if (vault.CreatorId != userInfo.Id) { throw new Exception("ACCESS DENIED"); }
        VaultKeepViewModel vaultKeep = _vks.Create(newVaultKeep);
        vaultKeep.Creator = userInfo;
        Keep keep = _ks.GetById(vaultKeep.KeepId);
        _ks.Keep(keep);
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeepViewModel>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        VaultKeepViewModel vaultKeep = _vks.GetById(id);
        Vault vault = _vs.GetById(vaultKeep.VaultId);
        if (vault.CreatorId != userInfo.Id) { throw new Exception("ACCESS DENIED"); }
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