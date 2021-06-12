using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShungoExpress.Web.Data.Entities
{
  public class User : IdentityUser
  {
    [Display(Name = "First Name")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [MaxLength(50, ErrorMessage = "The field {0} accept only {1} characters")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }
    
    [Display(Name = "Phone Number")]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    [Display(Name = "Email Confirmed")]
    public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

    [Display(Name = "Full Name")]
    public string FullName => $"{this.FirstName} {this.LastName}";

    [NotMapped]
    [Display(Name = "Is Admin?")]
    public bool IsAdmin { get; set; }
  }
}
