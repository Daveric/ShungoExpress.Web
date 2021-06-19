namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class LoginViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Nombre de usuario")]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
  }
}
