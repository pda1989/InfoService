namespace InfoService
{
    public abstract class ServiceCommand
    {
        public abstract OutputMessage Execute(InputMessage message);

        public virtual OutputMessage GetErrorMessage(string errorMessage)
        {
            return new OutputMessage { Result = false, Message = errorMessage };
        }
    }
}
