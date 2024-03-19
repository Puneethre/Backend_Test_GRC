using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class CategoryMaster
{
    public int CategoryId { get; set; }

    public string? Category { get; set; }

    public char? Editable { get; set; }

    public char? Active { get; set; }

    public virtual ICollection<CategoryListMaster> CategoryListMasters { get; set; } = new List<CategoryListMaster>();
}
