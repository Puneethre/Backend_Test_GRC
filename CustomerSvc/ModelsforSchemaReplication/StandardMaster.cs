using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class StandardMaster
{
    public int Id { get; set; }

    public string? ShortName { get; set; }

    public string? Name { get; set; }

    public char? IsActive { get; set; }

    public int? GovrId { get; set; }

    public int? Levels { get; set; }

    public string? LevelNames { get; set; }

    public int? NoOfControls { get; set; }

    public virtual GovernanceMaster? Govr { get; set; }
}
