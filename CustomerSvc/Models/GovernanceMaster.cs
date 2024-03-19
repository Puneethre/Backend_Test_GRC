using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class GovernanceMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Abbr { get; set; }

    public char? Active { get; set; }

    public virtual ICollection<StandardMaster> StandardMasters { get; set; } = new List<StandardMaster>();
}
