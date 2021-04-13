using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Dapper;

namespace Repositories
{
   public class ContractorsRepository
   {
      private readonly IDbConnection _db;
      public ContractorsRepository(IDbConnection db)
      {
         _db = db;
      }


      internal IEnumerable<Contractor> GetAll()
      {
         string sql = @"
         SELECT * FROM contractors";
         return _db.Query<Contractor>(sql);
      }
      //      1 SELECT 
      //      4 wlist.*,
      //      6 prof.*
      //      2 FROM contractors wlist
      //      5 JOIN profiles prof ON wlist.creatorId = prof.id
      //      3 WHERE wlist.id = @id";
      internal Contractor GetById(int id)
      {
         string sql = @" 
         SELECT * FROM contractors
         WHERE id = @id";
         return _db.Query<Contractor>(sql, new { id }).FirstOrDefault();
      }

      internal Contractor Create(Contractor newContractor)
      {
         string sql = @"
         INSERT INTO contractors 
         (name, contact, info) 
         VALUES 
         (@Name, @Contact, @Info);
         SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newContractor);
         newContractor.Id = id;
         return newContractor;
      }

      internal Contractor Edit(Contractor updated)
      {
         string sql = @"
        UPDATE contractors
        SET
         name = @Name,
         contact = @Contact,
         info = @Info
        WHERE id = @Id;";
         _db.Execute(sql, updated);
         return updated;
      }

      internal void Delete(int id)
      {
         string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
         _db.Execute(sql, new { id });
      }
   }
}