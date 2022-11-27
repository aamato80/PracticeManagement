using FluentValidation;
using FluentValidation.Results;
using DossierManagement.Api.DTOs;
using System;

namespace DossierManagement.Api.Validators
{
    public class DossierDtoValidator : AbstractValidator<DossierDto>
    {
        public DossierDtoValidator()

        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FiscalCode).NotEmpty().MaximumLength(16);
            RuleFor(x => x.BirthDate).NotEmpty().GreaterThan(new DateTime(1,1,1));
            RuleFor(x => x.Attachment).NotEmpty();
        }
    }
}
