using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Motorized: IEntity
  {
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "Apellido")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    [Required]
    public string LastName { set; get; }

    [Display(Name = "Motorizado")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string DisplayName { get; set; }

    [Display(Name = "Placa")]
    public string Plate { get; set; }

    [Display(Name = "Cédula")]
    public int IdNumber { get; set; }

    [Display(Name = "Descripción")]
    public string Description { get; set; }

    [Display(Name = "Disponible?")]
    public bool Available { get; set; }
  }
}
