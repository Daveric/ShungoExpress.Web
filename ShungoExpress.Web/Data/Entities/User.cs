using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class User : IdentityUser
  {
    [Required]
    [Display(Name = "First Name")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string LastName { get; set; }

    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [DataType(DataType.Url)]
    [Display(Name = "Link address")]
    public string AddressUrl { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    [Display(Name = "Email Confirmed")]
    public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

    [Display(Name = "Full Name")]
    public string FullName => $"{this.FirstName} {this.LastName}";

    public string Role { get; set; }
  }
}
