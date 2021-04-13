using System.Threading.Tasks;
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class AssignmentsController : ControllerBase
   {
      private readonly AssignmentsService _service;

      public AssignmentsController(AssignmentsService service)
      {
         _service = service;
      }

      [HttpPost]
      public ActionResult<Assignment> Create([FromBody] Assignment newWLP)
      {
         try
         {
            return Ok(_service.Create(newWLP));
         }
         catch (System.Exception err)
         {
            return BadRequest(err.Message);
         }
      }

      [HttpDelete("{id}")]
      public ActionResult<string> Delete(int id)
      {
         try
         {
            _service.Delete(id);
            return Ok("deleted");
         }
         catch (System.Exception err)
         {
            return BadRequest(err.Message);
         }
      }
   }
}