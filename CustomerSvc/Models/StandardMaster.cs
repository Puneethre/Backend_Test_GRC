using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class StandardMaster
{
    public int Id { get; set; }

    public string StdAbbr { get; set; } = null!;

    public string? Name { get; set; }

    public char? Active { get; set; }

    public int? GovrId { get; set; }

    public int? Levels { get; set; }

    public string? LevelNames { get; set; }

    public int? NoOfControls { get; set; }

    public virtual GovernanceMaster? Govr { get; set; }

    public virtual ICollection<SysLicenseMaster> SysLicenseMasters { get; set; } = new List<SysLicenseMaster>();
}
