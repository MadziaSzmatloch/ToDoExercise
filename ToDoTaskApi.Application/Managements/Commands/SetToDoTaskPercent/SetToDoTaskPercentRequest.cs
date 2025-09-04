using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;

namespace ToDoTaskApi.Application.Managements.Commands.SetToDoTaskPercent
{
    public record SetToDoTaskPercentRequest(Guid id, int percentage) : IRequest<ToDoTaskDTO>;
}
