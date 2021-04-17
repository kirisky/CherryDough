namespace CherryDough.Domain.Commands.Validations
{
    public class RemoveItemCommandValidation : ItemValidation<RemoveItemCommand>
    {
        public RemoveItemCommandValidation()
        {
            ValidateId();
        }
    }
}