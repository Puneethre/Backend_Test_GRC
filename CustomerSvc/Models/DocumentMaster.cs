using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class DocumentMaster
{
    public int Id { get; set; }

    public string? Document { get; set; }

    public virtual ICollection<ActivityMaster> ActivityMasterOutputDocumentNavigations { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ActivityMaster> ActivityMasterRefDocumentNavigations { get; set; } = new List<ActivityMaster>();
}
