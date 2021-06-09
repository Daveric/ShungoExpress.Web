using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Order
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }


    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Cost { get; set; }


    public Motorized Motorized{ get; set; }
  }
}
