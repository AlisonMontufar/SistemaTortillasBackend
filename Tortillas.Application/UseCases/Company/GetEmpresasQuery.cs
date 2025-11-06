using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Application.Dtos.Company;
using Tortillas.Domain.Entities;

namespace Tortillas.Application.UseCases.Company
{
    public record GetEmpresasQuery() : IRequest<List<EmpresaDto>>;


}
