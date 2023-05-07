using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Pet
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
