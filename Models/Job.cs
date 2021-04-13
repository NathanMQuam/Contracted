using System.ComponentModel.DataAnnotations;

namespace Models
{
   public class Job
   {
      public int Id { get; set; }
      [Required]
      [MinLength(3)]
      public string Title { get; set; }
      public string Description { get; set; }
      public int Pay { get; set; }
   }

   public class JobContractorViewModel : Job
   {
      public int AssignmentId { get; set; }
   }
}