using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToDoTaskApi.Domain.Entities
{
    // Domain entity representing a To-Do task in the system.
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        //Property to track task expiration date, validation ensures the value is never in the past
        private DateTime _expirationDate;
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value < DateTime.UtcNow.Date)
                    throw new ValidationException("Expiration date cannot be in the past.");
                _expirationDate = value;
            }
        }

        //Property to track task completion, validation ensures the value is always within bounds
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

        // Parameterless constructor for EF Core
        public ToDoTask() { }

        // Simplified constructor for creating new tasks with defaults
        public ToDoTask(string title, string description, DateTime expirationDate)
        {
            if (expirationDate < DateTime.UtcNow)
                throw new ValidationException("Expiration date cannot be in the past.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            PercentOfCompletness = 0;
            IsCompleted = false;
        }
    }
}
