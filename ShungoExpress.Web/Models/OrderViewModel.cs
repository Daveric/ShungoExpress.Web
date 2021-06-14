using Microsoft.AspNetCore.Mvc.Rendering;
using ShungoExpress.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ShungoExpress.Web.Models
{
  public class OrderViewModel
  {
    [Required]
    [Display(Name = "Order date")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Delivery date")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeliveryDate { get; set; }

    [Display(Name = "Client")]
    [StringLength(50, ErrorMessage = "You must select a client or create a new one")]
    public string ClientName { get; set; }

    public IEnumerable<SelectListItem> Clients { get; set; }

    [Required]
    [MaxLength(160, ErrorMessage = "The field {0} accept only {1} characters")]
    public string Description { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
    public decimal Cost { get; set; }

    [Display(Name = "Motorized")]
    [Range(1, int.MaxValue, ErrorMessage = "You must select a motorized or create a new one")]
    public int MotorizedId { get; set; }

    public IEnumerable<SelectListItem> Motorizeds { get; set; }
  }
}
