﻿namespace Books.MinimalApi.Helpers;
public class MinimalValidator : IMinimalValidator
{
    public ValidationResult Validate<T>(T model)
    {
        var result = new ValidationResult()
        {
            IsValid = true
        };
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var customAttributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);
            foreach (var attribute in customAttributes)
            {
                var validationAttribute = attribute as ValidationAttribute;
                if (validationAttribute != null)
                {
                    var propertyValue = property.CanRead ? property.GetValue(model) : null;
                    var isValid = validationAttribute.IsValid(propertyValue);

                    if (!isValid)
                    {
                        if (result.Errors.ContainsKey(property.Name))
                        {
                            var errors = result.Errors[property.Name].ToList();
                            errors.Add(validationAttribute.FormatErrorMessage(property.Name));
                            result.Errors[property.Name] = errors.ToArray();
                        }
                        else
                        {
                            result.Errors.Add(property.Name, new string[] { validationAttribute.FormatErrorMessage(property.Name) });
                        }

                        result.IsValid = false;
                    }
                }
            }
        }

        return result;
    }
}
