namespace WebCiro.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Branch { get; set; }
        public string Customer { get; set; }
        public string ProductName { get; set; }
        public string ProdutGruop { get; set; }
        public decimal Amount { get; set; }
        public string? PurchasedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
