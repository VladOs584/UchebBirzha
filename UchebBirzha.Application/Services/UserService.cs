using UchebBirzha.Application.Interfaces;
using UchebBirzha.Application.DTOs.Users;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Application.Mappers;

namespace UchebBirzha.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            IBidRepository bidRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _bidRepository = bidRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            ValidateEntityExists(user, "User", id);

            return user.ToUserDto();
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            ValidateEntityExists(user, "User", email);

            return user.ToUserDto();
        }

        public async Task<UserProfileDto> GetUserProfileAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            ValidateEntityExists(user, "User", id);

            // Считаем статистику
            var createdTasksCount = user.CreatedTasks.Count;
            var activeBidsCount = await _bidRepository.GetBidsByExecutorAsync(id)
                .ContinueWith(t => t.Result.Count(b => !b.IsAccepted));

            return user.ToUserProfileDto(createdTasksCount, activeBidsCount);
        }

        public async Task<IReadOnlyList<UserDto>> GetTopExecutorsAsync(int count = 10)
        {
            var executors = await _userRepository.GetExecutorsWithHighRatingAsync(4.0m);
            return executors
                .OrderByDescending(e => e.Rating)
                .ThenByDescending(e => e.CompletedTasksCount)
                .Take(count)
                .Select(e => e.ToUserDto())
                .ToList();
        }

        public async Task<UserDto> UpdateUserProfileAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            ValidateEntityExists(user, "User", userId);

            user.UpdateProfile(updateUserDto.FirstName, updateUserDto.LastName, updateUserDto.AvatarUrl);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.ToUserDto();
        }

        public async Task UpdateUserRatingAsync(int executorId, decimal newRating)
        {
            var user = await _userRepository.GetByIdAsync(executorId);
            ValidateEntityExists(user, "User", executorId);

            ValidateBusinessRule(
                user.Role == Domain.Enums.UserRole.Executor,
                "Рейтинг можно обновлять только для исполнителей"
            );

            user.SetRating(newRating);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}