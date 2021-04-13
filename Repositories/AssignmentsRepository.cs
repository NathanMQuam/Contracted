using System;
using System.Data;
using Models;
using Dapper;

namespace Repositories
{
   public class AssignmentsRepository
   {
      private readonly IDbConnection _db;
      public AssignmentsRepository(IDbConnection db)
      {
         _db = db;
      }
      internal Assignment Create(Assignment newAssignment)
      {
         string sql = @"
      INSERT INTO assignments 
      (jobId, contractorId) 
      VALUES 
      (@JobId, @ContractorId);
      SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newAssignment);
         newAssignment.Id = id;
         return newAssignment;
      }

      internal void Delete(int id)
      {
         string sql = "DELETE FROM assignments WHERE id = @id LIMIT 1;";
         _db.Execute(sql, new { id });

      }
   }
}