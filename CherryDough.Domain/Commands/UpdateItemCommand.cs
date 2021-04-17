using System;
using CherryDough.Domain.Commands.Validations;

namespace CherryDough.Domain.Commands
{
    public class UpdateItemCommand : ItemCommand
    {
        public UpdateItemCommand(Guid id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}