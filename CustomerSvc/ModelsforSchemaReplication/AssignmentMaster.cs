using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class AssignmentMaster
{
    public int Id { get; set; }

    public int? ActivityMasterId { get; set; }

    public int? DoerCliUserId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Doerstatus { get; set; }

    public char? AuditCheck { get; set; }

    public char? ApprovalStatus { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int? ApproverCliUserId { get; set; }

    public string? DoerComments { get; set; }

    public string? ApproverComments { get; set; }

    public string? EvidenceDetails { get; set; }

    public virtual ActivityMaster? ActivityMaster { get; set; }

    public virtual ClientUserInfo? ApproverCliUser { get; set; }

    public virtual ClientUserInfo? DoerCliUser { get; set; }
}
