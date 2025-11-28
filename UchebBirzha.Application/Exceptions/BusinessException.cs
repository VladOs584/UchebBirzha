namespace UchebBirzha.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class NotFoundException : BusinessException
    {
        public NotFoundException(string entityName, object id)
            : base($"{entityName} with id {id} not found") { }

        public NotFoundException(string entityName, string identifier)
            : base($"{entityName} with identifier '{identifier}' not found") { }
    }

    public class ValidationException : BusinessException
    {
        public ValidationException(string message) : base(message) { }
    }

    public class UnauthorizedException : BusinessException
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}