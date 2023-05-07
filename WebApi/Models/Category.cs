using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
