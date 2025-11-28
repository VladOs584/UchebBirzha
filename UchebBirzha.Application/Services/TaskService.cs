using System.Linq;
using UchebBirzha.Application.Common;
using UchebBirzha.Application.DTOs.Tasks;
using UchebBirzha.Application.Interfaces;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Enums;
using UchebBirzha.Domain.Interfaces;
using SystemTask = System.Threading.Tasks.Task; // ← ДОБАВЬТЕ ЭТУ СТРОКУ
using TaskEntity = UchebBirzha.Domain.Entities.Task; // ← И ЭТУ

namespace UchebBirzha.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDetailDto> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDetailDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                CustomerEmail = task.Customer.Email,
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                ExecutorEmail = task.Executor?.Email,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskDetailDto> GetTaskDetailAsync(int id, int currentUserId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            if (task.CustomerId != currentUserId && task.ExecutorId != currentUserId)
                return null;

            return await GetTaskByIdAsync(id);
        }

        public async Task<IReadOnlyList<TaskDto>> GetAllOpenTasksAsync()
        {
            var tasks = await _taskRepository.GetOpenTasksAsync();
            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();
        }

        public async Task<IReadOnlyList<TaskDto>> GetUserTasksAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return new List<TaskDto>();

            IReadOnlyList<TaskEntity> tasks = user.Role == UserRole.Customer
                ? await _taskRepository.GetTasksByCustomerAsync(userId)
                : await _taskRepository.GetTasksByExecutorAsync(userId);

            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();
        }

        public async Task<IReadOnlyList<TaskDto>> GetTasksByCategoryAsync(int categoryId)
        {
            var tasks = await _taskRepository.GetTasksByCategoryAsync(categoryId);
            return tasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, int customerId)
        {
            var customer = await _userRepository.GetByIdAsync(customerId);
            if (customer == null || customer.Role != UserRole.Customer)
                return null;

            var category = await _categoryRepository.GetByIdAsync(createTaskDto.CategoryId);
            if (category == null) return null;

            var task = new TaskEntity(
                createTaskDto.Title,
                createTaskDto.Description,
                createTaskDto.Budget,
                createTaskDto.Deadline,
                customerId,
                createTaskDto.CategoryId
            );

            await _taskRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CategoryId = task.CategoryId,
                CategoryName = category.Name,
                BidCount = 0,
                IsOverdue = false,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskDto> UpdateTaskAsync(int taskId, UpdateTaskDto updateTaskDto, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.CustomerId != userId || task.Status != TaskWorkStatus.Open)
                return null;

            task.UpdateDetails(updateTaskDto.Title, updateTaskDto.Description, updateTaskDto.Budget, updateTaskDto.Deadline);
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            };
        }

        // Теперь правильно возвращает SystemTask
        public async SystemTask DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.CustomerId != userId || task.Status != TaskWorkStatus.Open)
                return;

            task.ChangeStatus(TaskWorkStatus.Cancelled);
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        // Теперь правильно возвращает SystemTask
        public async SystemTask AssignExecutorAsync(int taskId, int executorId, int customerId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.CustomerId != customerId || task.Status != TaskWorkStatus.Open)
                return;

            var executor = await _userRepository.GetByIdAsync(executorId);
            if (executor == null || executor.Role != UserRole.Executor)
                return;

            task.SetExecutor(executorId);
            task.ChangeStatus(TaskWorkStatus.InProgress);
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        // Теперь правильно возвращает SystemTask
        public async SystemTask CompleteTaskAsync(int taskId, int customerId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.CustomerId != customerId || task.Status != TaskWorkStatus.InProgress || !task.ExecutorId.HasValue)
                return;

            task.ChangeStatus(TaskWorkStatus.Completed);
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        // Теперь правильно возвращает SystemTask
        public async SystemTask CancelTaskAsync(int taskId, int customerId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.CustomerId != customerId)
                return;

            task.ChangeStatus(TaskWorkStatus.Cancelled);
            _taskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultDto<TaskDto>> SearchTasksAsync(TaskSearchDto searchDto)
        {
            var tasks = await _taskRepository.GetOpenTasksAsync();
            var filteredTasks = tasks.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                var term = searchDto.SearchTerm.ToLower();
                filteredTasks = filteredTasks.Where(t =>
                    t.Title.ToLower().Contains(term) ||
                    t.Description.ToLower().Contains(term));
            }

            if (searchDto.CategoryId.HasValue)
                filteredTasks = filteredTasks.Where(t => t.CategoryId == searchDto.CategoryId);

            if (searchDto.MinBudget.HasValue)
                filteredTasks = filteredTasks.Where(t => t.Budget >= searchDto.MinBudget);

            if (searchDto.MaxBudget.HasValue)
                filteredTasks = filteredTasks.Where(t => t.Budget <= searchDto.MaxBudget);

            if (searchDto.Status.HasValue)
                filteredTasks = filteredTasks.Where(t => t.Status == searchDto.Status);

            if (searchDto.OnlyWithBids == true)
                filteredTasks = filteredTasks.Where(t => t.Bids.Any());

            var totalCount = filteredTasks.Count();
            var pagedTasks = filteredTasks
                .Skip(searchDto.Skip)
                .Take(searchDto.Take)
                .ToList();

            var taskDtos = pagedTasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();

            return new PagedResultDto<TaskDto>
            {
                Items = taskDtos,
                TotalCount = totalCount,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize
            };
        }

        public async Task<PagedResultDto<TaskDto>> GetTasksByCustomerAsync(int customerId, PagedRequestDto request)
        {
            var tasks = await _taskRepository.GetTasksByCustomerAsync(customerId);
            var pagedTasks = tasks.Skip(request.Skip).Take(request.Take).ToList();

            var taskDtos = pagedTasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();

            return new PagedResultDto<TaskDto>
            {
                Items = taskDtos,
                TotalCount = tasks.Count,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResultDto<TaskDto>> GetTasksByExecutorAsync(int executorId, PagedRequestDto request)
        {
            var tasks = await _taskRepository.GetTasksByExecutorAsync(executorId);
            var pagedTasks = tasks.Skip(request.Skip).Take(request.Take).ToList();

            var taskDtos = pagedTasks.Select(task => new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Budget = task.Budget,
                Deadline = task.Deadline,
                Status = task.Status,
                CustomerId = task.CustomerId,
                CustomerName = $"{task.Customer.FirstName} {task.Customer.LastName}",
                ExecutorId = task.ExecutorId,
                ExecutorName = task.Executor != null ? $"{task.Executor.FirstName} {task.Executor.LastName}" : null,
                CategoryId = task.CategoryId,
                CategoryName = task.Category.Name,
                BidCount = task.Bids.Count,
                IsOverdue = task.Deadline < DateTime.UtcNow && task.Status != TaskWorkStatus.Completed,
                CreatedAt = task.CreatedAt
            }).ToList();

            return new PagedResultDto<TaskDto>
            {
                Items = taskDtos,
                TotalCount = tasks.Count,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}