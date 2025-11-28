using UchebBirzha.Domain.Common;
using UchebBirzha.Domain.Enums;

namespace UchebBirzha.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; private set; }
        public string? AvatarUrl { get; private set; }
        public decimal? Rating { get; private set; }
        public int CompletedTasksCount { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        public virtual ICollection<Task> CreatedTasks { get; private set; } = new List<Task>();
        public virtual ICollection<Bid> Bids { get; private set; } = new List<Bid>();
        public virtual ICollection<Review> ReceivedReviews { get; private set; } = new List<Review>();

        private User() { }

        public User(string email, string passwordHash, string firstName, string lastName, UserRole role)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Role = role;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;

            if (role == UserRole.Executor)
            {
                Rating = 0;
                CompletedTasksCount = 0;
            }
        }

        
        public void UpdateProfile(string firstName, string lastName, string? avatarUrl = null)
        {
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
        }

        public void SetRating(decimal? rating)
        {
            Rating = rating;
        }

        public void IncrementCompletedTasks()
        {
            CompletedTasksCount++;
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}