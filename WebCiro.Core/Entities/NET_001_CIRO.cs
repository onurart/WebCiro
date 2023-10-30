using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.Entities
{
    public class NET_001_CIRO:BaseEntity
    {
        public string City { get; set; }
        public string StockCategories { get; set; }
        public string AccountCode { get; set; }
        public string Customer { get; set; }
        public string AUXCODE2 { get; set; }
        public string StokName { get; set; }
        public string SpeCode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TlNet { get; set; }

        public string COUNTRY { get; set; }

         }
}
