using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class RoleType
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ClientRoleMaster> ClientRoleMasters { get; set; } = new List<ClientRoleMaster>();
}
