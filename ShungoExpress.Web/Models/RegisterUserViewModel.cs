namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class RegisterNewUserViewModel
  {
    [Required]
    [Display(Name = "Nombre")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Apellido")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }

    [Display(Name = "Direccioón")]
    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [Display(Name = "Celular")]
    [MaxLength(20, ErrorMessage = "The field {0} accept only {1} characters")]
    public string PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Contraseña")]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirmar contraseña")]
    [Compare("Password")]
    public string Confirm { get; set; }
  }
}
