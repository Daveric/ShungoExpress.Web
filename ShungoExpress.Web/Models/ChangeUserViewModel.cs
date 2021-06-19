namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class ChangeUserViewModel
  {
    [Required]
    [Display(Name = "Nombre")]
    public string FirstName { get; set; }

    [Display(Name = "Apellido")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Dirección")]
    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [Display(Name = "Celular")]
    [MaxLength(20, ErrorMessage = "The field {0} accept only {1} characters")]
    public string PhoneNumber { get; set; }
  }
}
