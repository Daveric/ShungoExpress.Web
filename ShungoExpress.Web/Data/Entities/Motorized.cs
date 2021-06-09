using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Motorized
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Is Available?")]
    public bool Available { get; set; }
  }
}
