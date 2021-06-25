namespace ExamHelperLibrary.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public class ObjectAttributeValidator
    {
        public ValidatorResult Validate(object obj)
        {
            var result = new ValidatorResult();

            if (obj == null)
            {
                result.Errors.Add("Entry object is null.");
                
                return result;
            }

            var objectType = obj.GetType();
            var properties = objectType.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var propertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(obj);
                var attributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
                
                foreach (var validationAttribute in attributes)
                {
                    var isValid = validationAttribute.IsValid(value);

                    if (!isValid)
                    {
                        var errorMessage = validationAttribute.FormatErrorMessage(propertyName);
                        result.Errors.Add(errorMessage);
                    }
                }
            }

            return result;
        }
    }
}