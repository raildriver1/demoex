using System;
using System.Collections.Generic;

namespace WpfApp3.Elements;

public partial class Usersorder
{
    public int IdUserOrder { get; set; }

    public int? UserId { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
