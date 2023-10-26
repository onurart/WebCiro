using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany
{
    public sealed class CreateCompanyRequest: IRequest<CreateCompanyResponse>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }
        public string? TaxDepartment { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? ServerName { get; set; }
        public string? DatabaseName { get; set; }
        public string? ServerUserId { get; set; }
        public string? ServerPassword { get; set; }
        public string? ClientApiUrl { get; set; }
        public bool? IsSync { get; set; } = false;
        public int? TimeHour { get; set; }
    }
}
