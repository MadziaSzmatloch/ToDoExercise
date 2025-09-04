using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;

namespace ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask
{
    public record UpdateToDoTaskRequest(ToDoTaskDTO task) : IRequest<ToDoTaskDTO>;
}
