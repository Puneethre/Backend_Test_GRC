using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysUserLogin
{
    public int SysUserId { get; set; }

    public string? LoginUserId { get; set; }

    public string? LoginEmailId { get; set; }

    public string? Pwd { get; set; }

    public string? Name { get; set; }

    public string? Phoneno { get; set; }

    public string? Mfacode { get; set; }

    public DateOnly? LastloginDatetime { get; set; }

    public int? NoofloginAttempts { get; set; }

    public int? SysRoleId { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? CreatedDatetime { get; set; }

    public string? Uid { get; set; }

    public int? Newuser { get; set; }

    public virtual ICollection<AssignmentMaster> AssignmentMasterApprovers { get; set; } = new List<AssignmentMaster>();

    public virtual ICollection<AssignmentMaster> AssignmentMasterUsers { get; set; } = new List<AssignmentMaster>();

    public virtual ICollection<ClientRoleMaster> ClientRoleMasters { get; set; } = new List<ClientRoleMaster>();

    public virtual ICollection<ClientUserInfo> ClientUserInfoCreatedByNavigations { get; set; } = new List<ClientUserInfo>();

    public virtual ICollection<ClientUserInfo> ClientUserInfoSysUsers { get; set; } = new List<ClientUserInfo>();

    public virtual ICollection<SysCustomerInfo> SysCustomerInfos { get; set; } = new List<SysCustomerInfo>();

    public virtual SysUserLogin? CreatedByNavigation { get; set; }

    public virtual ICollection<SysUserLogin> InverseCreatedByNavigation { get; set; } = new List<SysUserLogin>();

    public virtual RoleType? NewuserNavigation { get; set; }

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterApprovedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterCreatedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterEditedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual SysRoleMaster? SysRole { get; set; }

    public virtual ICollection<SysRoleMaster> SysRoleMasters { get; set; } = new List<SysRoleMaster>();

    public virtual ICollection<SysCustomerInfo> Customers { get; set; } = new List<SysCustomerInfo>();
}
