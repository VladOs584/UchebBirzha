using Microsoft.EntityFrameworkCore;
using UchebBirzha.Domain.Common;
using UchebBirzha.Domain.Entities;
using UchebBirzha.Domain.Enums;
using UchebBirzha.Domain.Interfaces;
using UchebBirzha.Infrastructure.Repositories;

namespace UchebBirzha.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets - переименовываем чтобы избежать конфликта
        public DbSet<User> UserSet => Set<User>();
        public DbSet<Domain.Entities.Task> TaskSet => Set<Domain.Entities.Task>();
        public DbSet<Bid> BidSet => Set<Bid>();
        public DbSet<Category> CategorySet => Set<Category>();
        public DbSet<Review> ReviewSet => Set<Review>();
        public DbSet<TaskAttachment> TaskAttachmentSet => Set<TaskAttachment>();

        // Реализация IUnitOfWork
        public ITaskRepository Tasks => new TaskRepository(this);
        public IUserRepository Users => new UserRepository(this);
        public IBidRepository Bids => new BidRepository(this);
        public ICategoryRepository Categories => new CategoryRepository(this);
        public IReviewRepository Reviews => new ReviewRepository(this);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Конфигурация User
            builder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.AvatarUrl)
                    .HasMaxLength(500);

                entity.Property(u => u.Rating)
                    .HasColumnType("decimal(3,2)");

                entity.Property(u => u.CompletedTasksCount)
                    .IsRequired();

                entity.Property(u => u.CreatedAt)
                    .IsRequired();

                entity.Property(u => u.LastLoginAt)
                    .IsRequired(false);

                entity.Property(u => u.IsActive)
                    .IsRequired();

                // Конвертация enum
                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (UserRole)Enum.Parse(typeof(UserRole), v)
                    );

                // Индексы
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.IsActive);
                entity.HasIndex(u => u.Role);
                entity.HasIndex(u => u.Rating);

                // Связи
                entity.HasMany(u => u.CreatedTasks)
                    .WithOne(t => t.Customer)
                    .HasForeignKey(t => t.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.Bids)
                    .WithOne(b => b.Executor)
                    .HasForeignKey(b => b.ExecutorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.ReceivedReviews)
                    .WithOne(r => r.Executor)
                    .HasForeignKey(r => r.ExecutorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация Task
            builder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(t => t.Budget)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(t => t.Deadline)
                    .IsRequired();

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                // Конвертация enum
                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => (TaskWorkStatus)Enum.Parse(typeof(TaskWorkStatus), v)
                    );

                // Внешние ключи
                entity.Property(t => t.CustomerId)
                    .IsRequired();

                entity.Property(t => t.CategoryId)
                    .IsRequired();

                entity.Property(t => t.ExecutorId)
                    .IsRequired(false);

                // Связи
                entity.HasOne(t => t.Customer)
                    .WithMany(u => u.CreatedTasks)
                    .HasForeignKey(t => t.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Executor)
                    .WithMany()
                    .HasForeignKey(t => t.ExecutorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Category)
                    .WithMany(c => c.Tasks)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.Bids)
                    .WithOne(b => b.Task)
                    .HasForeignKey(b => b.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Attachments)
                    .WithOne(a => a.Task)
                    .HasForeignKey(a => a.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Индексы
                entity.HasIndex(t => t.Status);
                entity.HasIndex(t => t.Deadline);
                entity.HasIndex(t => t.CreatedAt);
                entity.HasIndex(t => t.CustomerId);
                entity.HasIndex(t => t.ExecutorId);
                entity.HasIndex(t => t.CategoryId);
            });

            // Конфигурация Bid
            builder.Entity<Bid>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.ProposedPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(b => b.Comment)
                    .HasMaxLength(500);

                entity.Property(b => b.CreatedAt)
                    .IsRequired();

                entity.Property(b => b.IsAccepted)
                    .IsRequired();

                // Внешние ключи
                entity.Property(b => b.TaskId)
                    .IsRequired();

                entity.Property(b => b.ExecutorId)
                    .IsRequired();

                // Связи
                entity.HasOne(b => b.Task)
                    .WithMany(t => t.Bids)
                    .HasForeignKey(b => b.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Executor)
                    .WithMany(u => u.Bids)
                    .HasForeignKey(b => b.ExecutorId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Индексы
                entity.HasIndex(b => b.TaskId);
                entity.HasIndex(b => b.ExecutorId);
                entity.HasIndex(b => b.IsAccepted);
                entity.HasIndex(b => b.CreatedAt);
            });

            // Конфигурация Category
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Description)
                    .HasMaxLength(200);

                // Связи
                entity.HasMany(c => c.Tasks)
                    .WithOne(t => t.Category)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Индекс
                entity.HasIndex(c => c.Name)
                    .IsUnique();
            });

            // Конфигурация Review
            builder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Rating)
                    .IsRequired();

                entity.Property(r => r.Comment)
                    .HasMaxLength(1000);

                entity.Property(r => r.CreatedAt)
                    .IsRequired();

                // Внешние ключи
                entity.Property(r => r.TaskId)
                    .IsRequired();

                entity.Property(r => r.AuthorId)
                    .IsRequired();

                entity.Property(r => r.ExecutorId)
                    .IsRequired();

                // Валидация рейтинга
                entity.HasCheckConstraint("CK_Review_Rating", "[Rating] >= 1 AND [Rating] <= 5");

                // Связи
                entity.HasOne(r => r.Task)
                    .WithMany()
                    .HasForeignKey(r => r.TaskId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Author)
                    .WithMany()
                    .HasForeignKey(r => r.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Executor)
                    .WithMany(u => u.ReceivedReviews)
                    .HasForeignKey(r => r.ExecutorId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Индексы
                entity.HasIndex(r => r.ExecutorId);
                entity.HasIndex(r => r.TaskId)
                    .IsUnique(); // Один отзыв на задачу
                entity.HasIndex(r => r.Rating);
                entity.HasIndex(r => r.CreatedAt);
            });

            // Конфигурация TaskAttachment
            builder.Entity<TaskAttachment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(a => a.FilePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(a => a.FileSize)
                    .IsRequired();

                entity.Property(a => a.UploadedAt)
                    .IsRequired();

                // Внешние ключи
                entity.Property(a => a.TaskId)
                    .IsRequired();

                // Связи
                entity.HasOne(a => a.Task)
                    .WithMany(t => t.Attachments)
                    .HasForeignKey(a => a.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Индексы
                entity.HasIndex(a => a.TaskId);
                entity.HasIndex(a => a.UploadedAt);
            });

            // Seed данных для категорий
            var categories = new[]
            {
                new Category("Математика", "Задачи по математике, алгебре, геометрии"),
                new Category("Программирование", "Задачи по программированию на различных языках"),
                new Category("Физика", "Задачи по физике и механике"),
                new Category("Химия", "Задачи по химии и биохимии"),
                new Category("Английский язык", "Задачи по английскому языку и переводы"),
                new Category("Экономика", "Задачи по экономике и финансам"),
                new Category("История", "Задачи по истории и обществознанию"),
                new Category("Другое", "Прочие учебные задачи")
            };

            // Устанавливаем ID через reflection (только для seed данных)
            for (int i = 0; i < categories.Length; i++)
            {
                var category = categories[i];
                var idProperty = typeof(BaseEntity).GetProperty("Id",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                idProperty?.SetValue(category, i + 1);
            }

            builder.Entity<Category>().HasData(categories);
        }

        // УПРОЩЕННЫЙ SaveChangesAsync - без установки дат
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Даты теперь устанавливаются в конструкторах сущностей
            return await base.SaveChangesAsync(cancellationToken);
        }

        // Явная реализация SaveChangesAsync для IUnitOfWork
        async Task<int> IUnitOfWork.SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }
    }
}