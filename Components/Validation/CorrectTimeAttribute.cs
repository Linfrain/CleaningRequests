using System.ComponentModel.DataAnnotations;

namespace CleaningRequests.Components.Validation
{
    public class CorrectTimeAttribute : ValidationAttribute
    {
        string dateProperty;
        public CorrectTimeAttribute(string dateProperty)
        {
            this.dateProperty = dateProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not TimeOnly time)
                return ValidationResult.Success;

            var workStart = new TimeOnly(8, 0);
            var workEnd = new TimeOnly(20, 0);

            if (time < workStart || time > workEnd)
            {
                return new ValidationResult("Время должно быть с 08:00 до 20:00.");
            }

            var request_date = validationContext.ObjectType.GetProperty(dateProperty) ?? null;
            var date = request_date?.GetValue(validationContext.ObjectInstance);

            if (date == null || request_date?.PropertyType != typeof(DateOnly))
                return ValidationResult.Success;

            if ((DateOnly)date == DateOnly.FromDateTime(DateTime.Today))
            {
                var minTime = TimeOnly.FromDateTime(DateTime.Now).AddHours(1);
                if (minTime >= workEnd)
                {
                    return new ValidationResult("На сегодня время уже закончилось. Выберите другой день.");
                }

                if (time.AddMinutes(1) < minTime)
                {
                    return new ValidationResult($"Для сегодняшнего дня время должно быть не ранее {minTime:HH\\:mm}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
