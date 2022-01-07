using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using System;

namespace keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeepViewModel GetById(int id)
    {
      VaultKeepViewModel vaultKeep = _repo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("Invalid id");
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
      _repo.Delete(id);
    }

  }
}