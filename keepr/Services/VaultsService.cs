using keepr.Models;
using keepr.Repositories;
using System;
using System.Collections.Generic;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault GetById(int id)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null) { throw new Exception("invalid id"); }
      return vault;
    }

    internal List<Vault> GetVaultsByProfile(string id)
    {
      return _repo.GetVaultsByProfile(id);
    }
    internal List<Vault> GetMyVaults(string id)
    {
      return _repo.GetMyVaults(id);
    }

    internal Vault Edit(Vault update)
    {
      Vault vault = GetById(update.Id);
      if (vault.CreatorId != update.CreatorId) { throw new Exception("ACCESS DENIED"); }
      vault.Name = update.Name != null && update.Name.Trim().Length > 0 ? update.Name : vault.Name;
      vault.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : vault.Description;
      vault.IsPrivate = update.IsPrivate != null ? update.IsPrivate : vault.IsPrivate;
      _repo.Edit(vault);
      return vault;
    }

    internal void Delete(int id, string userId)
    {
      Vault vault = GetById(id);
      if (vault.CreatorId != userId) { throw new Exception("ACCESS DENIED"); }
      _repo.Delete(id);
    }

  }
}