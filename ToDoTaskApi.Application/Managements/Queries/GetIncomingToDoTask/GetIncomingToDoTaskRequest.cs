using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Domain.Enums;

namespace ToDoTaskApi.Application.Managements.Queries.GetIncomingToDoTask
{
    public record GetIncomingToDoTaskRequest(ToDoTaskPeriod period) : IRequest<IEnumerable<ToDoTaskDTO>>;
}
