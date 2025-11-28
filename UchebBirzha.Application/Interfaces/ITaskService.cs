using UchebBirzha.Application.Common;
using UchebBirzha.Application.DTOs.Tasks;

namespace UchebBirzha.Application.Interfaces
{
    public interface ITaskService
    {
        
        Task<TaskDetailDto> GetTaskByIdAsync(int id);
        Task<TaskDetailDto> GetTaskDetailAsync(int id, int currentUserId);
        Task<IReadOnlyList<TaskDto>> GetAllOpenTasksAsync();
        Task<IReadOnlyList<TaskDto>> GetUserTasksAsync(int userId);
        Task<IReadOnlyList<TaskDto>> GetTasksByCategoryAsync(int categoryId);

    
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, int customerId);
        Task<TaskDto> UpdateTaskAsync(int taskId, UpdateTaskDto updateTaskDto, int userId);
        Task DeleteTaskAsync(int taskId, int userId);

       
        Task AssignExecutorAsync(int taskId, int executorId, int customerId);
        Task CompleteTaskAsync(int taskId, int customerId);
        Task CancelTaskAsync(int taskId, int customerId);
      
        Task<PagedResultDto<TaskDto>> SearchTasksAsync(TaskSearchDto searchDto);
        Task<PagedResultDto<TaskDto>> GetTasksByCustomerAsync(int customerId, PagedRequestDto request);
        Task<PagedResultDto<TaskDto>> GetTasksByExecutorAsync(int executorId, PagedRequestDto request);
    }
}