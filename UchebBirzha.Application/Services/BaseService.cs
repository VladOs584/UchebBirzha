
using UchebBirzha.Application.Exceptions;


namespace UchebBirzha.Application.Services
{
    public abstract class BaseService
    {
        protected void ValidateEntityExists<T>(T entity, string entityName, object id)
        {
            if (entity == null)
                throw new NotFoundException(entityName, id);
        }

        protected void ValidateBusinessRule(bool condition, string errorMessage)
        {
            if (!condition)
                throw new BusinessException(errorMessage);
        }

        protected void ValidateUserIsOwner(int ownerId, int currentUserId, string entityName)
        {
            if (ownerId != currentUserId)
                throw new UnauthorizedException($"Only owner can modify {entityName}");
        }

        protected void ValidateUserIsOwnerOrExecutor(int ownerId, int? executorId, int currentUserId, string entityName)
        {
            if (ownerId != currentUserId && executorId != currentUserId)
                throw new UnauthorizedException($"Only owner or executor can modify {entityName}");
        }

        protected void ValidateDeadline(DateTime deadline)
        {
            if (deadline <= DateTime.UtcNow)
                throw new ValidationException("Deadline must be in the future");
        }

        protected void ValidateBudget(decimal budget)
        {
            if (budget <= 0)
                throw new ValidationException("Budget must be positive");
        }
    }
}