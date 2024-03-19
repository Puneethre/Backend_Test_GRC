using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysLicenseMaster
{
    public int Id { get; set; }

    public DateOnly? StartOrRenewalDate { get; set; }

    public int? ContractPeriodInMonths { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? CustomerId { get; set; }

    public int? StandardId { get; set; }

    public char? Approved { get; set; }

    public string? ContractDocuments { get; set; }

    public string? Remarks { get; set; }

    public char? Active { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public int? EditedBy { get; set; }

    public DateOnly? EditedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateOnly? ApprovedDate { get; set; }

    public virtual SysUserLogin? ApprovedByNavigation { get; set; }

    public virtual SysUserLogin? CreatedByNavigation { get; set; }

    public virtual SysCustomerInfo? Customer { get; set; }

    public virtual SysUserLogin? EditedByNavigation { get; set; }

    public virtual StandardMaster? Standard { get; set; }
}
