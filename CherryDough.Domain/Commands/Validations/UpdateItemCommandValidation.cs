namespace CherryDough.Domain.Commands.Validations
{
    public class UpdateItemCommandValidation : ItemValidation<UpdateItemCommand>
    {
        public UpdateItemCommandValidation()
        {
            ValidateId();
        }
    }
}