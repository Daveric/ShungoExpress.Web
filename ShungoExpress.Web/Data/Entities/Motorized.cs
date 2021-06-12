using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Motorized: IEntity
  {
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Is Available?")]
    public bool Available { get; set; }
  }
}
