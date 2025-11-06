using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tortillas.Application.UseCases.Company
{
    public record DeleteEmpresaCommand(int Id) : IRequest;

}
