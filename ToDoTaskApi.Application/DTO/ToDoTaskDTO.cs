using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTaskApi.Application.DTO
{
    public class ToDoTaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int PercentOfCompletness { get; set; }
        public bool IsCompleted { get; set; }
    }
}
