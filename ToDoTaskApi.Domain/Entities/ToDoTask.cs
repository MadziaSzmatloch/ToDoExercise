using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToDoTaskApi.Domain.Entities
{
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        private int _percentOfCompletness;
        public int PercentOfCompletness
        {
            get => _percentOfCompletness;
            set
            {
                if (value < 0 || value > 100)
                    throw new ValidationException("Value must be between 0 and 100.");
                _percentOfCompletness = value;
            }
        }

        public bool IsCompleted { get; set; }

        public ToDoTask() { }

        public ToDoTask(Guid id, string title, string description, DateTime expirationDate, int percentOfCompletness, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            PercentOfCompletness = percentOfCompletness; 
            IsCompleted = isCompleted;
        }

        public ToDoTask(string title, string description, DateTime expirationDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            PercentOfCompletness = 0;
            IsCompleted = false;
        }
    }
}
