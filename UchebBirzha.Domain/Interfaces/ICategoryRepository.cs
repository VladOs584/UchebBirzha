using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}