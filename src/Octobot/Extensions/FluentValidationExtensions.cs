using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Octobot.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> EnumExact<T, TEnum>(this IRuleBuilder<T, string> ruleBuilder) where TEnum : struct, IConvertible
        {
            return ruleBuilder
                .Must(propertyValue => propertyValue == null || propertyValue.IsEnumExact<TEnum>())
                .WithMessage("'{PropertyName}' must be a valid " + typeof(TEnum).Name + ".");
        }

        public static bool IsEnumExact<TEnum>(this string propertyValue) where TEnum : struct, IConvertible
        {
            return propertyValue.TryParseToEnumExact(out TEnum enumValue);
        }

        public static IRuleBuilderOptions<T, string> EnumExact<T, TEnum>(this IRuleBuilder<T, string> ruleBuilder, params TEnum[] possibleValues) where TEnum : struct, IConvertible
        {
            return ruleBuilder
                .Must(propertyValue => propertyValue == null || propertyValue.IsEnumExact(possibleValues))
                .WithMessage("'{PropertyName}' must be one of: " + string.Join(", ", possibleValues) + ".");
        }

        public static bool IsEnumExact<TEnum>(this string propertyValue, params TEnum[] validValues) where TEnum : struct, IConvertible
        {
            var parsed = propertyValue.TryParseToEnumExact(out TEnum enumValue);
            return parsed && validValues.Contains(enumValue);
        }

        public static IRuleBuilderOptions<T, string[]> ArgumentPropertyFileExists<T>(this IRuleBuilder<T, string[]> ruleBuilder, int index)
        {
            return ruleBuilder.Must(x => ArgumentPropertyFileExists(x, index)).WithMessage($"File for argument:{index} is missingn or does not exist");
        }

        public static IRuleBuilderOptions<T, string> FileExists<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(FileExists).WithMessage("File does not exist");
        }
        private static bool ArgumentPropertyFileExists(IReadOnlyList<string> file, int index)
        {
            return file.Count > 0 && FileExists(file[index]);
        }

        private static bool FileExists(string file)
        {
            return File.Exists(file);
        }

        public static IRuleBuilderOptions<T, string> Numeric<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsNumeric).WithMessage("{PropertyName} must be numeric.");
        }

        public static bool IsNumeric(string propertyValue)
        {
            return double.TryParse(propertyValue, out double result);
        }

        public static IRuleBuilderOptions<T, string> Integer<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsInteger).WithMessage("{PropertyName} must be an integer.");
        }

        public static IRuleBuilderOptions<T, string> Long<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsLong).WithMessage("{PropertyName} must be an long.");
        }

        public static bool IsLong(this string propertyValue)
        {
            return long.TryParse(propertyValue, out long result);
        }

        public static bool IsNonNegativeLong(this string propertyValue)
        {
            long result;
            return long.TryParse(propertyValue, out result) && result >= 0;
        }

        public static bool IsNegativeLong(this string propertyValue, bool allowZero = false)
        {
            if (!IsLong(propertyValue))
                return false;

            var value = propertyValue.AsLong();
            return value < 0 || (value == 0 && allowZero);
        }

        public static bool IsInteger(this string propertyValue)
        {
            int result;
            return int.TryParse(propertyValue, out result);
        }

        // TODO: Should return true if propertValue is null to be consistent with standard FluentValidation pattern
        public static IRuleBuilderOptions<T, string> Boolean<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsBoolean).WithMessage("'{PropertyName}' must be a boolean.");
        }
        
        public static IRuleBuilderOptions<T, string> BooleanOrYesNo<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(propertyValue => IsBoolean(propertyValue) || IsYesNo(propertyValue)).WithMessage("'{PropertyName}' must be a boolean.");
        }

        public static bool IsYesNo(this string propertyValue)
        {
            if (propertyValue.IsEmpty())
                return false;
            if (string.Equals(propertyValue, "Y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(propertyValue, "Yes", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(propertyValue, "N", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(propertyValue, "No", StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public static bool IsBoolean(this string propertyValue)
        {
            if (propertyValue.IsEmpty())
                return false;
            bool result;
            return bool.TryParse(propertyValue, out result);
        }

        public static IRuleBuilderOptions<T, string> InRangeInclusive<T>(this IRuleBuilder<T, string> ruleBuilder,
            int min, int max)
        {
            return ruleBuilder.Must(x => IsInRange(x, min, max))
                .WithMessage("{PropertyName} must be in range {0} - {1}", min, max);
        }

        public static bool IsInRange(string propertyValue, int min, int max)
        {
            int.TryParse(propertyValue, out int result);
            return result >= min && result <= max;
        }
        
        public static bool IsPositiveDecimal(this string propertyValue)
        {
            return double.TryParse(propertyValue, out double result) && result > 0;
        }
        
        public static IRuleBuilderOptions<T, string> NonNegativeLong<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(propertyValue => propertyValue == null || propertyValue.IsNonNegativeLong())
                .WithMessage("{PropertyName} must be a positive numeric value with no decimal places.");
        }

        public static IRuleBuilderOptions<T, string> NegativeLong<T>(this IRuleBuilder<T, string> ruleBuilder, bool allowZero = false)
        {
            return ruleBuilder.Must(propertyValue => propertyValue == null || propertyValue.IsNegativeLong(allowZero))
                .WithMessage("{PropertyName} must be a negative numeric value with no decimal places.");
        }
        
        public static IRuleBuilderOptions<T, string> Id<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsId).WithMessage("{PropertyName} must be a long Id greater than zero.");
        }

        public static bool IsId(this string propertyValue)
        {
            var parsedToLong = long.TryParse(propertyValue, out long value);
            return propertyValue != null && propertyValue.IsNotEmpty() && propertyValue.All(Char.IsDigit) &&
                   parsedToLong && value > 0;
        }

        public static IRuleBuilderOptions<T, string> TimeSpanHourMinute<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsHourMinuteTimeSpan)
                    .WithMessage("{PropertyName} must be a valid timespan in {0} format.", "hh:mm");
        }

        public static IRuleBuilderOptions<T, string> TimeSpan<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsTimeSpan);
        }

        public static bool IsHourMinuteTimeSpan(this string propertyValue)
        {
            return System.TimeSpan.TryParseExact(propertyValue, @"hh\:mm", null, TimeSpanStyles.None, out TimeSpan result);
        }

        public static bool IsTimeSpan(this string value)
        {
            return System.TimeSpan.TryParse(value, out TimeSpan result);
        }

        public static IRuleBuilderOptions<T, string> DateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsDateTime).WithMessage("{PropertyName} is not in a reconizable date time format");
        }
        
        public static bool IsDateTime(this string propertyValue)
        {
            return System.DateTime.TryParse(propertyValue, out DateTime result);
        }
        
        public static void ThrowResults(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public static ValidationResult Combine(this ValidationResult result, ValidationResult other)
        {
            return Combine(new[] { result, other });
        }

        public static ValidationResult Combine(this IEnumerable<ValidationResult> results)
        {
            return new ValidationResult(from r in results
                from failure in r.Errors
                select failure);
        }

        public static IRuleBuilderOptions<T, TProperty> FailsWhen<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty, bool> predicate)
        {
            return ruleBuilder.Must((x, val, propertyValidatorContext) => !predicate(x, val));
        }
    }
}