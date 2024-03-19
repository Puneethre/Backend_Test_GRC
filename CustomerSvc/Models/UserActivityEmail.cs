using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class UserActivityEmail
{
    public int ActivityId { get; set; }

    public string? EmailCodeToActivity { get; set; }
}
