using System;
using System.Collections.Generic;

namespace WpfApp3.Elements;

public partial class Prodname
{
    public int IdProdName { get; set; }

    public string ProdName1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}
