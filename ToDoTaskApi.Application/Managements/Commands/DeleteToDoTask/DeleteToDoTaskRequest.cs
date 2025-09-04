using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ToDoTaskApi.Application.Managements.Commands.DeleteToDoTask
{
    public record DeleteToDoTaskRequest(Guid id) : IRequest<Unit>;
}
