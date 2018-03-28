using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using Swashbuckle.Swagger;

namespace MicroElements.Swashbuckle.FluentValidation
{
    /// <summary>
    /// Swagger <see cref="ISchemaFilter"/> that uses FluentValidation validators instead System.ComponentModel based attributes.
    /// </summary>
    public class FluentValidationRules : ISchemaFilter
    {
        private readonly IValidatorFactory _factory;

        /// <summary>
        /// Default constructor with DI
        /// </summary>
        /// <param name="factory"></param>
        public FluentValidationRules(IValidatorFactory factory)
        {
            _factory = factory;
        }

        public void Apply(Schema model, SchemaRegistry context, Type type)
        {
            // use IoC or FluentValidatorFactory to get AbstractValidator<T> instance
            var validator = _factory.GetValidator(type);
            if (validator == null) return;
            if (model.required == null)
                model.required = new List<string>();

            var validatorDescriptor = validator.CreateDescriptor();
            foreach (var key in model.properties.Keys)
            {
                foreach (var propertyValidator in validatorDescriptor.GetValidatorsForMember(ToPascalCase(key)))
                {
                    if (propertyValidator is NotNullValidator
                        || propertyValidator is NotEmptyValidator)
                        model.required.Add(key);

                    if (propertyValidator is LengthValidator lengthValidator)
                    {
                        if (lengthValidator.Max > 0)
                            model.properties[key].maxLength = lengthValidator.Max;

                        model.properties[key].minLength = lengthValidator.Min;
                    }

                    if (propertyValidator is RegularExpressionValidator expressionValidator)
                        model.properties[key].pattern = expressionValidator.Expression;

                    // Add more validation properties here;
                }
            }
        }

        /// <summary>
        /// To convert case as swagger may be using lower camel case
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static string ToPascalCase(string inputString)
        {
            // If there are 0 or 1 characters, just return the string.
            if (inputString == null) return null;
            if (inputString.Length < 2) return inputString.ToUpper();
            return inputString.Substring(0, 1).ToUpper() + inputString.Substring(1);
        }
    }
}