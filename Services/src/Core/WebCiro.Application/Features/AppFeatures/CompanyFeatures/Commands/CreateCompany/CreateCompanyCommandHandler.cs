using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Application.Services.AppServices;
using WebCiro.Domain.AppEntities;

namespace WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany
{
    public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyRequest, CreateCompanyResponse>
    {
        private readonly ICompanyService _companyService;

        public CreateCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<CreateCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {

            Company company = await _companyService.GetCompanyByName(request.Name);
            if (company != null) throw new Exception("Bu firma daha önce açilmiş");
            await _companyService.CreateCompany(request);
            return new();
        }
    }
}
