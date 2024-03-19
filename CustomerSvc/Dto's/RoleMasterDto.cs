namespace GRCServices.Dto_s
{
    public class GetRoleMasterDto
    {
        public int SysRoleId { get; set; }

        public string? RoleName { get; set; }

        //public int? RoleTypeId { get; set; }

        public string? Description { get; set; }

        public string? Comments { get; set; }

        public char? Active { get; set; }

    }

    public class AddRoleMasterDto
    {
        public string? RoleName { get; set; }

       // public int? RoleTypeId { get; set; }

        public string? Description { get; set; }

        public string? Comments { get; set; }

        public int? CreatedBy { get; set; }

        public char? Active { get; set; }

        public int? CustomerId { get; set; }

    }

    public class UpdateRoleMasterDto
    {
       // public int SysRoleId { get; set; }

        public string? RoleName { get; set; }

        public int? RoleTypeId { get; set; }

        public string? Description { get; set; }

        public string? Comments { get; set; }

        public int? CreatedBy { get; set; }

        public char? Active { get; set; }

        public int? CustomerId { get; set; }
    }

    public class RoleTypeLookup
    {
        public int RoleTypeId { get; set; }

        public string? RoleTypeDescription { get; set; }
    }

    public class RoleLookUp
    {
        public int clientRoleId { get; set; }

        public string? RoleName { get; set; }

    }
}
