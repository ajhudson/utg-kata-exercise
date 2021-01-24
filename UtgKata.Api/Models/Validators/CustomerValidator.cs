// <copyright file="CustomerValidator.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models.Validators
{
    using FluentValidation;
    using UtgKata.Lib;

    /// <summary>
    /// Validator for customer view model.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{UtgKata.Api.Models.CustomerViewModel}" />
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        private const string IsRequiredTemplate = "{0} is required";

        private const string CannotBeMoreThanTemplate = "{0} cannot be more than {1} characters";

        /// <summary>Initializes a new instance of the <see cref="CustomerValidator" /> class.</summary>
        public CustomerValidator()
        {
            this.RuleFor(x => x.Address1).MaximumLength(255).WithMessage(string.Format(CannotBeMoreThanTemplate, "Address 1", 255));
            this.RuleFor(x => x.Address2).MaximumLength(255).WithMessage(string.Format(CannotBeMoreThanTemplate, "Address 2", 255));
            this.RuleFor(x => x.Country).MaximumLength(50).WithMessage(string.Format(CannotBeMoreThanTemplate, "Country", 255));
            this.RuleFor(x => x.County).MaximumLength(50).WithMessage(string.Format(CannotBeMoreThanTemplate, "County", 50));

            this.RuleFor(x => x.CustomerRef)
                                .NotEmpty()
                                .WithMessage(string.Format(IsRequiredTemplate, "Customer Reference"))
                                .MaximumLength(50)
                                .WithMessage(string.Format(CannotBeMoreThanTemplate, "Customer Reference", 50));

            this.RuleFor(x => x.FirstName)
                                .NotEmpty()
                                .WithMessage(string.Format(IsRequiredTemplate, "First Name"))
                                .MaximumLength(50)
                                .WithMessage(string.Format(CannotBeMoreThanTemplate, "First Name", 50));

            this.RuleFor(x => x.LastName)
                                .NotEmpty()
                                .WithMessage(string.Format(IsRequiredTemplate, "Last Name"))
                                .MaximumLength(50)
                                .WithMessage(string.Format(CannotBeMoreThanTemplate, "Last Name", 50));

            this.RuleFor(x => x.PostCode)
                                .NotEmpty()
                                .WithMessage(string.Format(IsRequiredTemplate, "Post Code"))
                                .MaximumLength(50)
                                .WithMessage(string.Format(CannotBeMoreThanTemplate, "Post Code", 50))
                                .Matches(RegExHelper.UkPostCodePattern)
                                .WithMessage("Post code must be a valid UK postal code");
        }
    }
}
