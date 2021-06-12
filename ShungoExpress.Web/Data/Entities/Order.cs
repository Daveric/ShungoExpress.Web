using System;
using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Order: IEntity
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Order date")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Delivery date")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeliveryDate { get; set; }
    
    [Required]
    public Client Client { get; set; }

    [Required]
    [MaxLength(160, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Description { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Cost { get; set; }
    
    [Required]
    public Motorized Motorized{ get; set; }
  }
}
