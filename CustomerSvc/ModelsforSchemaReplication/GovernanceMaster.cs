using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class GovernanceMaster
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public char? IsActive { get; set; }

    public virtual ICollection<StandardMaster> StandardMasters { get; set; } = new List<StandardMaster>();
}
