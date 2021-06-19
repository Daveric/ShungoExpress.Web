using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Models
{
  public class ClientViewModel : ChangeUserViewModel
  {
    [DataType(DataType.Url)]
    [Display(Name = "Link dirección")]
    public string AddressUrl { get; set; }
  }
}
