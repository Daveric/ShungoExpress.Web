namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class ChangePasswordViewModel
  {
    [Required]
    [Display(Name = "Contraseña actual")]
    public string OldPassword { get; set; }

    [Required]
    [Display(Name = "Nueva contraseña")]
    public string NewPassword { get; set; }

    [Required]
    [Display(Name = "Confirmar contraseña")]
    [Compare("NewPassword")]
    public string Confirm { get; set; }
  }
}
