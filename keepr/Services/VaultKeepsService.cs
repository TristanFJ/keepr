using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using System;

namespace keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;
    private readonly KeepsService _ks;

    public VaultKeepsService(VaultKeepsRepository repo, KeepsService ks)
    {
      _repo = repo;
      _ks = ks;
    }

    internal VaultKeepViewModel GetById(int id)
    {
      VaultKeepViewModel vaultKeep = _repo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("Invalid id (vks)");
      }
      return vaultKeep;
    }

    internal VaultKeepViewModel Create(VaultKeepViewModel newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }

    internal List<VaultKeepViewModel> GetAll(int id)
    {
      return _repo.GetAll(id);
    }

    internal void Delete(int id, string userId)
    {
      VaultKeepViewModel vaultKeep = GetById(id);
      if (vaultKeep.CreatorId != userId) { throw new Exception("ACCESS DENIED"); }
      Keep keep = _ks.GetById(vaultKeep.KeepId);
      _ks.Unkeep(keep);
      _repo.Delete(id);
    }

  }
}