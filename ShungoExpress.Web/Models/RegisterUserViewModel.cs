namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class RegisterNewUserViewModel
  {
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }

    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [MaxLength(20, ErrorMessage = "The field {0} accept only {1} characters")]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string Confirm { get; set; }
  }
}
