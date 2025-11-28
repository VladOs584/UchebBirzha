using UchebBirzha.Application.DTOs.Auth;
using UchebBirzha.Application.Exceptions;
using UchebBirzha.Application.Interfaces;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Enums;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Application.Mappers;
using UchebBirzha.Infrastructure.Interfaces;

namespace UchebBirzha.Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Проверяем уникальность email
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new ValidationException("Пользователь с таким email уже существует");

            // Хешируем пароль
            var passwordHash = _passwordHasher.HashPassword(registerDto.Password);

            // Определяем роль на основе типа аккаунта
            var userRole = registerDto.AccountType == AccountType.Customer
                ? UserRole.Customer
                : UserRole.Executor;

            // Создаем пользователя
            var user = new User(
                email: registerDto.Email,
                passwordHash: passwordHash,
                firstName: registerDto.FirstName,
                lastName: registerDto.LastName,
                role: userRole
            );

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Генерируем токен
            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                User = user.ToUserInfoDto()
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            ValidateEntityExists(user, "User", loginDto.Email);

            if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new ValidationException("Неверный email или пароль");

            if (!user.IsActive)
                throw new ValidationException("Аккаунт деактивирован");

            // Обновляем последний логин
            user.UpdateLastLogin();
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Генерируем токен
            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                User = user.ToUserInfoDto()
            };
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            ValidateEntityExists(user, "User", userId);

            if (!_passwordHasher.VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
                throw new ValidationException("Текущий пароль неверен");

            var newPasswordHash = _passwordHasher.HashPassword(changePasswordDto.NewPassword);
            user.ChangePassword(newPasswordHash);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}