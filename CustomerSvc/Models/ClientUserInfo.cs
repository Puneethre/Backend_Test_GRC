using GRCServices.Models;
using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class ClientUserInfo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? CustomerId { get; set; }

    public int? SysUserId { get; set; }

    public int? CliRoleId { get; set; }

    public char? Status { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ClientRoleMaster? CliRole { get; set; }

    public virtual SysUserLogin? CreatedByNavigation { get; set; }

    public virtual SysCustomerInfo? Customer { get; set; }

    public virtual SysUserLogin? SysUser { get; set; }
}
