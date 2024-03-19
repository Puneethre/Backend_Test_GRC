using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class AssignmentMaster
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public int? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Doerstatus { get; set; }

    public char? AuditCheck { get; set; }

    public char? ApprovalStatus { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int? ApproverId { get; set; }

    public string? DoerComments { get; set; }

    public string? ApproverComments { get; set; }

    public string? EvidenceDetails { get; set; }

    public virtual ActivityMaster? Activity { get; set; }

    public virtual ICollection<ActivityMaster> ActivityMasters { get; set; } = new List<ActivityMaster>();

    public virtual SysUserLogin? Approver { get; set; }

    public virtual SysUserLogin? User { get; set; }
}
