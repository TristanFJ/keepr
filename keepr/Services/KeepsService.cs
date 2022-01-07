using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using System;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep keep = _repo.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid id");
      }
      return keep;
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep update)
    {
      Keep keep = GetById(update.Id);
      if (keep.CreatorId != update.CreatorId)
      {
        throw new Exception("ACCESS DENIED");
      }
      keep.Name = update.Name != null && update.Name.Trim().Length > 0 ? update.Name : keep.Name;
      keep.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : keep.Description;
      keep.Img = update.Img != null && update.Img.Trim().Length > 0 ? update.Img : keep.Img;
      keep.Views = update.Views != 0 ? update.Views : keep.Views;
      keep.Shares = update.Shares != 0 ? update.Shares : keep.Shares;
      keep.Keeps = update.Keeps != 0 ? update.Keeps : keep.Keeps;
      _repo.Edit(keep);
      return keep;
    }

    internal void Delete(int id, string userId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId != userId)
      { throw new Exception("ACCESS DENIED"); }
      _repo.Delete(id);
    }

  }
}