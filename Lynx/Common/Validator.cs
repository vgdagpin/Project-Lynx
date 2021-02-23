using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Lynx.Domain.ViewModels;

namespace Lynx
{
    public class Validator<T> where T : class
    {
        public ValidationResult ValidateUsing<TValidator>(T instance)
            where TValidator : AbstractValidator<T>
        {
            var context = new FluentValidation.ValidationContext<T>(instance);
            var validator = Activator.CreateInstance<TValidator>();

            return validator.Validate(context);
        }
    }
}
