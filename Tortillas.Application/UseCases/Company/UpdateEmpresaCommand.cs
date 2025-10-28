using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tortillas.Application.Dtos.Company;

namespace Tortillas.Application.UseCases.Company
{
    public record UpdateEmpresaCommand(EmpresaDto Empresa) : IRequest;
}
