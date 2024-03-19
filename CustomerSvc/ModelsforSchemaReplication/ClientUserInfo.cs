using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class ClientUserInfo
{
    public int CliUserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? CustomerId { get; set; }

    public int? SysUserId { get; set; }

    public int? CliRoleId { get; set; }

    public string? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<AssignmentMaster> AssignmentMasterApproverCliUsers { get; set; } = new List<AssignmentMaster>();

    public virtual ICollection<AssignmentMaster> AssignmentMasterDoerCliUsers { get; set; } = new List<AssignmentMaster>();

    public virtual ClientRoleMaster? CliRole { get; set; }
}
