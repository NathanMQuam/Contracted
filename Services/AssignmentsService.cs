using System;
using Models;
using Repositories;

namespace Services
{
   public class AssignmentsService
   {
      private readonly AssignmentsRepository _repo;

      public AssignmentsService(AssignmentsRepository repo)
      {
         _repo = repo;
      }

      internal Assignment Create(Assignment newAssignment)
      {
         //TODO if they are creating a assignment, make sure they are the creator of the list
         return _repo.Create(newAssignment);

      }

      internal void Delete(int id)
      {
         //NOTE getbyid to validate its valid and you are the creator
         _repo.Delete(id);
      }
   }
}