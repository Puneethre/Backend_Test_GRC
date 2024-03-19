using GRCServices.Models;
using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysCustomerInfo
{
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? Description { get; set; }

    public string? DbMapString { get; set; }

    public int? CreatedBy { get; set; }

    public char? Active { get; set; }

    public virtual ICollection<ClientUserInfo> ClientUserInfos { get; set; } = new List<ClientUserInfo>();

    public virtual SysUserLogin? CreatedByNavigation { get; set; }

    public virtual ICollection<SysLicenseMaster> SysLicenseMasters { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysUserLogin> SysUsers { get; set; } = new List<SysUserLogin>();
}
