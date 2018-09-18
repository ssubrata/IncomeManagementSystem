using System;
using System.Globalization;
using System.Windows.Controls;

namespace Desktop.Helper
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "তথ্য প্রদান করুন.")
                : ValidationResult.ValidResult;
        }
    }
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal val;
            var result = decimal.TryParse(value as string, out val);
            return result ? ValidationResult.ValidResult
                      : new ValidationResult(false, "শুধু মাত্র সংখ্যা তথ্য করুন");

        }
    }
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime val;
            var result = DateTime.TryParse(value.ToString(), out val);
            return result ? ValidationResult.ValidResult
                      : new ValidationResult(false, "তারিখ সঠিক ভাবে প্রদান করুন ");

        }
    }
    public class TextInputLengthCheck : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 0 && value.ToString().Length < 7)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Check Length");

            }
                
        }
    }
}
