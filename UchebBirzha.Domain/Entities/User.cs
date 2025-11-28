using UchebBirzha.Domain.Enums;
using UchebBirzha.Domain.Common;
using System.Security.Cryptography;

namespace UchebBirzha.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; private set; }
        public string? AvatarUrl { get; private set; }
        public decimal? Rating { get; private set; }
        public int CompletedTasksCount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        
        public virtual ICollection<Task> CreatedTasks { get; private set; } = new List<Task>();
        public virtual ICollection<Bid> Bids { get; private set; } = new List<Bid>();
        public virtual ICollection<Review> ReceivedReviews { get; private set; } = new List<Review>();

        private User() { }

        public User(string email, string firstName, string lastName, UserRole role)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreatedAt = DateTime.UtcNow;

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

        public void SetRating(decimal rating)
        {
            Rating = rating;
        }

        public void IncrementCompletedTasks()
        {
            CompletedTasksCount++;
        }
    }
}