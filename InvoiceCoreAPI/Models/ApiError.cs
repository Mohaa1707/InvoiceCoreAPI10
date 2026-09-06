namespace InvoiceCoreAPI.Models
{
    public class ApiError
    {
        public required string Code { get; set; }
        public required string Details { get; set; }
    }
}
