using System;
using System.Collections.Generic;

namespace Lab8_JulioSandoval.Models;

public partial class client
{
    public int clientid { get; set; }

    public string name { get; set; } = null!;

    public string email { get; set; } = null!;

    public virtual ICollection<order> orders { get; set; } = new List<order>();
}
