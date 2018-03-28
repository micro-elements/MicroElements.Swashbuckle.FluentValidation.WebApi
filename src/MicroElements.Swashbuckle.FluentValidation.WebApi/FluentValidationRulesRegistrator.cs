using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation;
using FluentValidation;
using FluentValidation.WebApi;
using MicroElements.Swashbuckle.FluentValidation;
using Swashbuckle.Application;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AspNetCore.Swagger
{
    public static class FluentValidationRulesRegistrator
    {
        /// <summary>
        /// Adds fluent validation rules to swagger.
        /// </summary>
        /// <param name="config">Swagger options.</param>
        /// <param name="factory">IValidatorFactory. If null then factory gets from <see cref="HttpConfiguration.Services"/></param>
        public static void AddFluentValidationRules(this SwaggerDocsConfig config, IValidatorFactory factory = null)
        {
            var validatorFactory = factory ?? ((FluentValidationModelValidatorProvider)GlobalConfiguration.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).FirstOrDefault(pr => pr is FluentValidationModelValidatorProvider))?.ValidatorFactory;

            if (validatorFactory != null)
                config.SchemaFilter(() => new FluentValidationRules(validatorFactory));
        }
    }
}