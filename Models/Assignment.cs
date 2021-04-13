using System.ComponentModel.DataAnnotations;

namespace Models
{
   public class Assignment
   {
      public int Id { get; set; }
      [Required]
      public int jobId { get; set; }
      [Required]
      public int contractorId { get; set; }
   }
}