using System;
using CherryDough.Domain.Commands.Validations;

namespace CherryDough.Domain.Commands
{
    public class RemoveItemCommand : ItemCommand
    {
        public RemoveItemCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}