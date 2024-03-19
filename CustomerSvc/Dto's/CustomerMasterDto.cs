namespace GRCServices.Dto_s
{
    public class GetCustomerMasterDto
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
    }
    public class AddCustomerMasterDto
    {
        public string? CustomerName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ContactName { get; set; }

        public string? ContactPhone { get; set; }

        public string? ContactEmail { get; set; }

        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public char? Active { get; set; }
    }
    public class UpdateCustomerMasterDto
    {
       // public string? CustomerName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ContactName { get; set; }

        public string? ContactPhone { get; set; }

        public string? ContactEmail { get; set; }

        public string? Description { get; set; }

        public int? CreatedBy { get; set; }

        public char? Active { get; set; }
    }
}
