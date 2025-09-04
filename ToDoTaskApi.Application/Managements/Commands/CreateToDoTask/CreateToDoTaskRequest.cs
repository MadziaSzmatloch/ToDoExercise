using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;

namespace ToDoTaskApi.Application.Managements.Commands.CreateToDoTask
{
    public record CreateToDoTaskRequest(
        string Title,
        string Description,
        DateTime ExpirationDate
        ) : IRequest<ToDoTaskDTO>;
}
