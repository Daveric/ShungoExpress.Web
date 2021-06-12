using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Client : User
  {
    [Display(Name = "Link address")]
    public string AddressUrl { get; set; }

  }
}
