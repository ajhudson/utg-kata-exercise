using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Lib;

namespace UtgKata.Api.Models.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        private const string IsRequiredTemplate = "{0} is required";

        private const string CannotBeMoreThanTemplate = "{0} cannot be more than {1} characters";

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
