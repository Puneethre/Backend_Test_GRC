﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class ActivityMaster
{
    public int Id { get; set; }

    public string? ActivityNameId { get; set; }

    public string? ActivityDescr { get; set; }

    public int? DoerRole { get; set; }

    public int? FrequencyId { get; set; }

    public int? Duration { get; set; }

    public int? RefDocumentId { get; set; }

    public int? OutputDocumentPath { get; set; }

    public int? TriggeringActivityNameId { get; set; }

    public int? ApproverRole { get; set; }

    public char? Auditable { get; set; }

    public int? PracticeId { get; set; }

    public int? HelpRef { get; set; }

    public string? IsActive { get; set; }

    public virtual ClientRoleMaster? ApproverRoleNavigation { get; set; }

    public virtual ICollection<AssignmentMaster> AssignmentMasters { get; set; } = new List<AssignmentMaster>();

    public virtual ClientRoleMaster? DoerRoleNavigation { get; set; }

    public virtual FrequencyMaster? Frequency { get; set; }

    public virtual DocumentMaster? OutputDocumentPathNavigation { get; set; }

    public virtual DocumentMaster? RefDocument { get; set; }

    public virtual ActivitiyNameMaster? TriggeringActivityName { get; set; }
}
