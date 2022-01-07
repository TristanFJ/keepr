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
          vk.id AS VaultKeepId,
          a.*
        FROM vaultkeeps vk
        JOIN keeps k on k.id = vk.keepId
        JOIN accounts a ON k.creatorId = a.id
        WHERE vk.Id = @id;
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
      string sql = @"
  SELECT
    k.*,
    vk.id AS VaultKeepId,
    a.*
  FROM vaultkeeps vk
  JOIN keeps k on k.id = vk.keepId
  JOIN accounts a ON k.creatorId = a.id
  WHERE vk.vaultId = @id;
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