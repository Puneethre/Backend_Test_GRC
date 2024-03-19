using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class CategoryListMaster
{
    public int ListId { get; set; }

    public int? CategoryId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public char? Active { get; set; }

    public virtual CategoryMaster? Category { get; set; }
}
