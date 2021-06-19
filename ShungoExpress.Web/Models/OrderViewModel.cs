using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShungoExpress.Web.Models
{
  public class OrderViewModel
  {
    [Required]
    [Display(Name = "Fecha Pedido")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Fecha Entrega")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeliveryDate { get; set; }

    [Display(Name = "Cliente")]
    [StringLength(50, ErrorMessage = "You must select a client or create a new one")]
    public string ClientName { get; set; }

    public IEnumerable<SelectListItem> Clients { get; set; }

    [Required]
    [Display(Name = "Descripción")]
    [MaxLength(160, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Costo")]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Cost { get; set; }

    [Display(Name = "Motorizado")]
    [Range(1, int.MaxValue, ErrorMessage = "You must select a motorized or create a new one")]
    public int MotorizedId { get; set; }

    public IEnumerable<SelectListItem> Motorizeds { get; set; }
  }
}
