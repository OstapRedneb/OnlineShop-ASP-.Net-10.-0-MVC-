using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class DateRangeAttribute : ValidationAttribute
    {
        public DateOnly MinDate = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly MaxDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(1);
        

        public DateRangeAttribute()
        {
            if (ErrorMessage is null)
                ErrorMessage = $"The date can't be before {MinDate.ToString()} and later than {MaxDate.ToString()}";
        }
        public DateRangeAttribute(DateOnly maxDate)
        {
            MaxDate = maxDate;

            if (ErrorMessage is null) 
                ErrorMessage = $"The date can't be before {MinDate.ToString()} and later than {MaxDate.ToString()}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);

            DateOnly date = DateOnly.FromDateTime((DateTime)value);

            if (date < MinDate || date > MaxDate)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
