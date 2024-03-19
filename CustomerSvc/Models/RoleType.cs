using System;
using System.Collections.Generic;

namespace GRCServices.Models;


public partial class RoleType
{
    public int RoleTypeId { get; set; }

    public string? RoleTypeDescription { get; set; }

    public virtual ICollection<ClientRoleMaster> ClientRoleMasters { get; set; } = new List<ClientRoleMaster>();

    public virtual ICollection<SysRoleMaster> SysRoleMasters { get; set; } = new List<SysRoleMaster>();

    public virtual ICollection<SysUserLogin> SysUserLogins { get; set; } = new List<SysUserLogin>();
}
