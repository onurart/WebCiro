using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using WebCiro.Domain.AppEntities;

namespace WebCiro.Application.Services.AppServices
{
    public interface ICompanyService
    {
        Task CreateCompany(CreateCompanyRequest request);
        //Task UpdateCompany(Company company, CancellationToken cancellationToken);
        //Task UpdatePhotoCompany(string id, string companylogo, CancellationToken cancellationToken);
        Task MigrateCompanyDatabases();
       Task<Company?> GetCompanyByName(string name);
        //IQueryable<Company> GetAll();
        //Task<Company> GetByIdAsync(string id);
    }
}
