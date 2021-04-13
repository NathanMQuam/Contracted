using System.ComponentModel.DataAnnotations;

namespace Models
{
   public class Contractor
   {
      public int Id { get; set; }
      [Required]
      [MinLength(3)]
      public string Name { get; set; }
      [Required]
      [MinLength(3)]
      public string Contact { get; set; }
      public string Info { get; set; }
   }
}