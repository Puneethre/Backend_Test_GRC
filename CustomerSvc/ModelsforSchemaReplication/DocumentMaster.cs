using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class DocumentMaster
{
    public int Id { get; set; }

    public string? Document { get; set; }

    public virtual ICollection<ActivityMaster> ActivityMasterOutputDocumentPathNavigations { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ActivityMaster> ActivityMasterRefDocuments { get; set; } = new List<ActivityMaster>();
}
