using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Models;
using Dapper;

namespace Repositories
{
   public class JobsRepository
   {
      private readonly IDbConnection _db;
      public JobsRepository(IDbConnection db)
      {
         _db = db;
      }


      internal IEnumerable<Job> GetAll()
      {
         string sql = @"
      SELECT 
      prod.*,
      prof.*
      FROM jobs prod
      JOIN profiles prof ON prod.creatorId = prof.id";
         // return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
         // {
         //    // job.Creator = profile;
         //    return job;
         // }, splitOn: "id");
         return _db.Query<Job>(sql);
      }

      internal Job GetById(int id)
      {
         string sql = @" 
      SELECT 
      prod.*,
      prof.*
      FROM jobs prod
      JOIN profiles prof ON prod.creatorId = prof.id
      WHERE prod.id = @id";
         // return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
         //    {
         //       job.Creator = profile;
         //       return job;
         //    }, new { id }, splitOn: "id").FirstOrDefault();
         return _db.Query<Job>(sql).FirstOrDefault();
      }

      internal Job Create(Job newJob)
      {
         string sql = @"
      INSERT INTO jobs 
      (title, description, price, creatorId) 
      VALUES 
      (@Title, @Description, @Price, @creatorId);
      SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newJob);
         newJob.Id = id;
         return newJob;
      }

      internal Job Edit(Job updated)
      {
         string sql = @"
        UPDATE jobs
        SET
         title = @Title,
         description = @Description,
         price = @Price
        WHERE id = @Id;";
         _db.Execute(sql, updated);
         return updated;
      }

      internal void Delete(int id)
      {
         string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1;";
         _db.Execute(sql, new { id });
      }

      // TODO: 
      // internal IEnumerable<WishListJobViewModel> GetJobsByListId(int id)
      // {
      //    string sql = @"SELECT 
      // p.*,
      // wlp.id AS WishListJobId
      // FROM wishlistjobs wlp
      // JOIN jobs p ON p.id = wlp.jobId
      // WHERE wishlistId = @id;";
      //    return _db.Query<WishListJobViewModel>(sql, new { id });
   }
}