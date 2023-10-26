using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Domain.Abstractions;

namespace WebCiro.Domain.CompanyEntities
{
    public sealed class Sale:Entity
    {
        public string Branch { get; set; }
        public string Customer { get; set; }
        public string ProductName { get; set; }
        public string ProdutGruop { get; set; }
        public decimal Amount { get; set; }
        public string PurchasedOn { get; set; }
        public bool IsActive { get; set; }
        public string CompanyId { get; set; }
    }
}
