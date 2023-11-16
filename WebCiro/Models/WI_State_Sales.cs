namespace WebCiro.Models
{
    public class WI_State_Sales
    {
        public long Id { get; set; }
        public string INVOICE_NUMBER { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public int INVOICE_YEAR { get; set; }
        public int INVOICE_MONTH { get; set; }
        public int INVOICE_DAY { get; set; }
        public int INVOICE_WEEK { get; set; }
        public string CITY { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string PRODUCT_CATEGORY { get; set; }
        public string PRODUCT_BRAND { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
        public double NET_AMOUNT { get; set; }
        public string PRODUCT_SUPPLIER { get; set; }
    }
}
