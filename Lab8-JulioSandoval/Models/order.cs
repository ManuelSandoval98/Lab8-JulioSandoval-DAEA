using System;
using System.Collections.Generic;

namespace Lab8_JulioSandoval.Models;

public partial class order
{
    public int orderid { get; set; }

    public int clientid { get; set; }

    public int productid { get; set; }

    public DateTime orderdate { get; set; }

    public virtual client client { get; set; } = null!;

    public virtual product product { get; set; } = null!;
}
