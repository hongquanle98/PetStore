using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? PetId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? ShipDate { get; set; }

    public string? Status { get; set; }

    public bool? Complete { get; set; }

    public virtual Pet? Pet { get; set; }
}
