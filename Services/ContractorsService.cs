using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
   public class ContractorsService
   {
      private readonly ContractorsRepository _repo;

      public ContractorsService(ContractorsRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Contractor> GetAll()
      {
         return _repo.GetAll();
      }

      internal Contractor GetById(int id)
      {
         var data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Contractor Create(Contractor newProd)
      {
         return _repo.Create(newProd);
      }

      internal Contractor Edit(Contractor updated)
      {
         var original = GetById(updated.Id);
         updated.Id = original.Id;
         updated.Name = updated.Name != null ? updated.Name : original.Name;
         updated.Contact = updated.Contact != null ? updated.Contact : original.Contact;
         updated.Info = updated.Info != null ? updated.Info : original.Info;
         return _repo.Edit(updated);
      }


      internal string Delete(int id)
      {
         var original = GetById(id);
         _repo.Delete(id);
         return "delorted";
      }
   }
}