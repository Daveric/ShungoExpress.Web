using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class User : IdentityUser
  {
    [Required]
    [Display(Name = "Nombre")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string FirstName { get; set; }

    [Display(Name = "Apellido")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string LastName { get; set; }

    [Display(Name = "Dirección")]
    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [DataType(DataType.Url)]
    [Display(Name = "Link dirección")]
    public string AddressUrl { get; set; }

    [Required]
    [Display(Name = "Celular")]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    [Display(Name = "Email confirmado")]
    public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
    
    public string Role { get; set; }

  }
}
