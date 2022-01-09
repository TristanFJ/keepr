using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {

    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a on v.creatorId = a.id
  WHERE v.id = @id
  ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal List<Vault> GetVaultsByProfile(string id)
    {
      string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a on v.creatorId = a.id
  WHERE v.creatorId = @id AND v.isPrivate = 0
  ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }).ToList();
    }
    internal List<Vault> GetMyVaults(string id)
    {
      string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a on v.creatorId = a.id
  WHERE v.creatorId = @id
  ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }).ToList();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults 
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal void Edit(Vault vault)
    {
      string sql = @"
  UPDATE vaults 
  SET
  name = @Name,
  description = @Description,
  isPrivate = @IsPrivate,
  creatorId = @CreatorId
  WHERE id = @Id
  ";
      _db.Execute(sql, vault);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }


  }
}