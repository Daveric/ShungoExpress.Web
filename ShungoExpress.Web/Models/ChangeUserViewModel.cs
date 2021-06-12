namespace ShungoExpress.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class ChangeUserViewModel
  {
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Address { get; set; }

    [MaxLength(20, ErrorMessage = "The field {0} accept only {1} characters")]
    public string PhoneNumber { get; set; }
  }
}
