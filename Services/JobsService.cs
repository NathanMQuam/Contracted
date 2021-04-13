using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
   public class JobsService
   {
      private readonly JobsRepository _repo;

      public JobsService(JobsRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Job> GetAll()
      {
         return _repo.GetAll();
      }

      internal Job GetById(int id)
      {
         var data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Job Create(Job newJob)
      {
         return _repo.Create(newJob);
      }

      internal Job Edit(Job updated)
      {
         var original = GetById(updated.Id);
         updated.Description = updated.Description != null ? updated.Description : original.Description;
         updated.Title = updated.Title != null && updated.Title.Length > 2 ? updated.Title : original.Title;
         return _repo.Edit(updated);
      }

      internal String Delete(int id)
      {
         var original = GetById(id);
         _repo.Delete(id);
         return "delorted";
      }

      // TODO: 
      // internal IEnumerable<WishListProductViewModel> GetProductsByListId(int id)
      // {
      //    return _repo.GetProductsByListId(id);
      // }
   }
}