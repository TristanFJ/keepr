using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {

    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"SELECT
       k.*,
       a.* 
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
       SELECT
       k.*,
       a.* 
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id
       WHERE k.id = @id";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }).FirstOrDefault();
    }



    internal Keep Create(Keep newKeep)
    {
      string sql = @"
  INSERT INTO keeps
  (name, description, img, views, shares, keeps, creatorId)
  VALUES
  (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
  SELECT LAST_INSERT_ID();
  ";

      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal void Edit(Keep keep)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps,
      creatorId = @CreatorId
      WHERE id = @Id
      ";
      _db.Execute(sql, keep);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}