using System;
using System.Collections.Generic;

namespace Lab8_JulioSandoval.Models;

public partial class product
{
    public int productid { get; set; }

    public string name { get; set; } = null!;

    public decimal price { get; set; }

    public virtual ICollection<order> orders { get; set; } = new List<order>();
}
