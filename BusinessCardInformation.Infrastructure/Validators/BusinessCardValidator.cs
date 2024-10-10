using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Validators
{
    public class BusinessCardValidator : AbstractValidator<BusinessCard>
    {
        private readonly IBusinessCardRepository _repository;
        public BusinessCardValidator(IBusinessCardRepository repository)
        {
            _repository = repository;
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(b => b.Gender)
                .NotEmpty().WithMessage("Gender is required.");

            RuleFor(b => b.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleFor(b => b.Email)
                 .NotEmpty().WithMessage("Email is required.")
                 .EmailAddress().WithMessage("Invalid email format.")
                 .MustAsync((businessCard, email, cancellationToken) =>
                     BeUniqueEmail(businessCard.Email, businessCard.Id, cancellationToken))
             .WithMessage("Email already exists.");

            RuleFor(b => b.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MustAsync((businessCard, phone, cancellationToken) =>
                    BeUniquePhone(businessCard.Phone, businessCard.Id, cancellationToken))
                .WithMessage("Phone number already exists.");

            RuleFor(b => b.Address)
                .NotEmpty().WithMessage("Address is required.");

           
        }

        private async Task<bool> BeUniqueEmail(string email, int cardId, CancellationToken cancellationToken)
        {
            return !await _repository.EmailExistsAsync(email, cardId);
        }

        private async Task<bool> BeUniquePhone(string phone, int cardId, CancellationToken cancellationToken)
        {
            return !await _repository.PhoneExistsAsync(phone, cardId);
        }
    }
}
