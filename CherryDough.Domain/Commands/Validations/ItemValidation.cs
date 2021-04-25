using System;
using CherryDough.Domain.Models;
using FluentValidation;

namespace CherryDough.Domain.Commands.Validations
{
    public abstract class ItemValidation<T> : AbstractValidator<T> where T : ItemCommand 
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is empty, this field is required and please give a value to it.")
                .Length(2, 100).WithMessage("Name must be between 2 to 150 characters.");
        }

        protected void ValidateCategory()
        {
            RuleFor(c => c.Category)
                .NotEmpty().WithMessage("Category is required, please give it a value.")
                .Length(2, 100).WithMessage("Category must be between 2 to 100 characters.");
        }

        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required, please give it a value.")
                .Length(2, 500).WithMessage("Description needs 2 to 500 characters");
        }
    }
}