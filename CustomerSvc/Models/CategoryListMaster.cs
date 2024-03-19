using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class CategoryListMaster
{
    public int ListId { get; set; }

    public int? CategoryId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public char? Active { get; set; }

    public virtual CategoryMaster? Category { get; set; }
}
