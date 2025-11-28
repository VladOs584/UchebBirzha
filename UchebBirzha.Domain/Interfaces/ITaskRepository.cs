using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Enums;
using Task = UchebBirzha.Domain.Entities.Task;

namespace UchebBirzha.Domain.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        Task<IReadOnlyList<Task>> GetOpenTasksAsync();
        Task<IReadOnlyList<Task>> GetTasksByCustomerAsync(int customerId);
        Task<IReadOnlyList<Task>> GetTasksByExecutorAsync(int executorId);
        Task<IReadOnlyList<Task>> GetTasksByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Task>> GetOverdueTasksAsync();
    }
}