using System;
using System.Collections.Generic;

namespace WpfApp3.Elements;

public partial class Prodorder
{
    public int IdProdOrder { get; set; }

    public int? ProdId { get; set; }

    public int? OrderId { get; set; }

    public int? Count { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Prod { get; set; }
}
