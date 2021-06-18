using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Motorized: IEntity
  {
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    [Required]
    public string FirstName { get; set; }
    
    public string LastName { set; get; }

    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string DisplayName { get; set; }

    public string Plate { get; set; }
    public int IdNumber { get; set; }
    public string Description { get; set; }

    [Display(Name = "Is Available?")]
    public bool Available { get; set; }
  }
}
