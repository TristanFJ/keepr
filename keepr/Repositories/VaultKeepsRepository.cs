using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeepViewModel GetById(int id)
    {
      string sql = @"
        SELECT
          k.*,
          k.id AS KeepId,
          vk.id AS VaultKeepId,
          v.id AS VaultId,
          a.*
        FROM vaultkeeps vk
        JOIN keeps k on k.id = vk.keepId
        JOIN vaults v on v.id = vk.vaultId
        JOIN accounts a ON k.creatorId = a.id
        WHERE vk.id = @id;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, profile) =>
      {
        vk.Creator = profile;
        return vk;
      }, new { id }).FirstOrDefault();
    }

    internal VaultKeepViewModel Create(VaultKeepViewModel newVaultKeep)
    {
      // NOTE I might need to change Id to VaultId
      string sql = @"
      INSERT INTO vaultkeeps
      (creatorId, keepId, vaultId)
      VALUES
      (@CreatorId, @KeepId, @VaultId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal List<VaultKeepViewModel> GetAll(int id)
    {
      // REVIEW Do I want the vault joined on the vault keep? Or is this good? I pass the tests, but isPrivate comes back null.
      string sql = @"
        SELECT
          k.*,
          k.id AS KeepId,
          vk.id AS VaultKeepId,
          v.id AS VaultId,
          a.*
        FROM vaultkeeps vk
        JOIN keeps k on k.id = vk.keepId
        JOIN vaults v on v.id = vk.vaultId
        JOIN accounts a ON k.creatorId = a.id
        WHERE v.id = @id;
  ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, profile) =>
      {
        vk.Creator = profile;
        return vk;
      }, new { id }).ToList();
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}