using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class ActivitiyNameMaster
{
    public int Id { get; set; }

    public string? ActivityName { get; set; }

    public virtual ICollection<ActivityMaster> ActivityMasters { get; set; } = new List<ActivityMaster>();
}
