using FluentValidation;
using PracticeManagement.Api.DTOs;
using System;

namespace PracticeManagement.Api.Validators
{
    public class PracticeDTOValidator : AbstractValidator<PracticeDTO>
    {
        public PracticeDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FiscalCode).NotEmpty().MaximumLength(16);
            RuleFor(x => x.BirthDate).NotEmpty().GreaterThan(new DateTime(1,1,1));
            RuleFor(x => x.Attachment).NotEmpty();
        }
    }
}
