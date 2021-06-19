using System;
using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Data.Entities
{
  public class Order: IEntity
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Fecha Pedido")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Fecha Entrega")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeliveryDate { get; set; }
    
    [Required]
    [Display(Name = "Cliente")]
    public User Client { get; set; }
    
    [Display(Name = "Dirección")]
    public string ClientAddress { get; set; }

    [Required]
    [Display(Name = "Descripción")]
    [MaxLength(160, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Motorizado")]
    public Motorized Motorized { get; set; }

    [Required]
    [Display(Name = "Costo")]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Cost { get; set; }
  }
}
