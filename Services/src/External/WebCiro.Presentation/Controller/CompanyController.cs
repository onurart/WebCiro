using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.MigrateCompanyDatabases;
using WebCiro.Presentation.Abstraction;

namespace WebCiro.Presentation.Controller
{
    public class CompanyController : ApiController
    {
        public CompanyController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateCompany(CreateCompanyRequest request)
        {
            CreateCompanyResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> MigrationCompanyDatabase()
        {

            MigrateCompanyDatabasesRequest request = new();
            MigrateCompanyDatabasesCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
