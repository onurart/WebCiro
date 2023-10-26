using ATBasketRobotServer.Persistance.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using WebCiro.Application.Services.AppServices;
using WebCiro.Domain.AppEntities;
using WebCiro.Persistance.Context;

namespace WebCiro.Persistance.Services.AppServices
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private static readonly Func<AppDbContext, string, Task<Company>> GetCompanyByNameCompiled
            = EF.CompileAsyncQuery((AppDbContext context, string name) => context.Set<Company>().FirstOrDefault
            (x => x.Name == name));
        public CompanyService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task CreateCompany(CreateCompanyRequest request)
        {
            Company company = _mapper.Map<Company>(request);
            company.Id=Guid.NewGuid().ToString();
            await _appDbContext.Set<Company>().AddAsync(company);
            await _appDbContext.SaveChangesAsync(); 
        }

        public async Task<Company?> GetCompanyByName(string name)
        {
            return await GetCompanyByNameCompiled(_appDbContext, name);
        }

        public async Task MigrateCompanyDatabases()
        {
            var companies = await _appDbContext.Set<Company>().ToListAsync();
            foreach (var company in companies)
            {
            var dbContext = new CompanyDbContext(company);
                dbContext.Database.Migrate();
            }
        }
        
    }
}
