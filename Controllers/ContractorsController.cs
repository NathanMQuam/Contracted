using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ContractorsController : ControllerBase
   {
      private readonly ContractorsService _service;
      private readonly JobsService _jserv;

      public ContractorsController(ContractorsService service, JobsService jserv)
      {
         _service = service;
         _jserv = jserv;

      }

      [HttpGet]
      public ActionResult<IEnumerable<Contractor>> Get()
      {
         try
         {
            return Ok(_service.GetAll());
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpGet("{id}")]  // NOTE '{}' signifies a var parameter
      public ActionResult<Contractor> Get(int id)
      {
         try
         {
            return Ok(_service.GetById(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }


      [HttpPost]
      // NOTE ANYTIME you need to use Async/Await you will return a Task
      public ActionResult<Contractor> Create([FromBody] Contractor newWList)
      {
         try
         {
            return Ok(_service.Create(newWList));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPut("{id}")]
      public ActionResult<Contractor> Edit([FromBody] Contractor updated, int id)
      {
         try
         {
            updated.Id = id;
            return Ok(_service.Edit(updated));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpDelete("{id}")]
      public ActionResult<Contractor> Delete(int id)
      {
         try
         {
            return Ok(_service.Delete(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpGet("{id}/jobs")]  // NOTE '{}' signifies a var parameter
      public ActionResult<IEnumerable<JobContractorViewModel>> GetJobsByContractorId(int id)
      {
         try
         {
            return Ok(_jserv.GetJobsByContractorId(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
   }
}