using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class ActivityMaster
{
    public int Id { get; set; }

    public string? ActivityName { get; set; }

    public string? ActivityDescr { get; set; }

    public int? DoerRole { get; set; }

    public int? Frequency { get; set; }

    public int? Duration { get; set; }

    public int? RefDocument { get; set; }

    public int? OutputDocument { get; set; }

    public int? TriggeringActivity { get; set; }

    public int? ApproverRole { get; set; }

    public char? Auditable { get; set; }

    public int? PracticeId { get; set; }

    public int? HelpRef { get; set; }

    public char? Active { get; set; }

    public virtual ClientRoleMaster? ApproverRoleNavigation { get; set; }

    public virtual ICollection<AssignmentMaster> AssignmentMasters { get; set; } = new List<AssignmentMaster>();

    public virtual ClientRoleMaster? DoerRoleNavigation { get; set; }

    public virtual FrequencyMaster? FrequencyNavigation { get; set; }

    public virtual DocumentMaster? OutputDocumentNavigation { get; set; }

    public virtual DocumentMaster? RefDocumentNavigation { get; set; }

    public virtual AssignmentMaster? TriggeringActivityNavigation { get; set; }
}
