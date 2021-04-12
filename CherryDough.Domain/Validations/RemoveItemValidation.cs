using CherryDough.Domain.Models;

namespace CherryDough.Domain.Validations
{
    public class RemoveItemValidation : ItemValidation<Item>
    {
        public RemoveItemValidation()
        {
            ValidateId();
        }
    }
}