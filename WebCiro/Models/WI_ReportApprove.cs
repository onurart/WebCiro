namespace WebCiro.Models
{
    public class WI_ReportApprove
    {
        public int Id { get; set; }
        public string SalesmanName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string City { get; set; }
        public string ProductName { get; set; }
        public decimal TotalTl { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }
        public string PartBrand { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
    }
}
