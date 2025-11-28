using UchebBirzha.Domain.Entities;

namespace UchebBirzha.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllCategoriesAsync();
        Task<IReadOnlyList<Category>> GetPopularCategoriesAsync(int count = 10);
    }
}