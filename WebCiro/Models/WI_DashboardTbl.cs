namespace WebCiro.Models
{
    public class WI_DashboardTbl
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string City { get; set; }
        public string ProductName { get; set; }
        public double TotalTl { get; set; }
        public string VehicleType { get; set; }
        public string VehicleBrand { get; set; }
        public string PartBrand { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int Rn { get; set; }


    }
}
