using System;
using System.Collections.Generic;

namespace WpfApp3.Elements;

public partial class Supplier
{
    public int IdSup { get; set; }

    public string SupName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
