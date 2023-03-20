using Api.DTO;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class CategoryService
    {
        private readonly CategoryContext _context;

        public CategoryService(CategoryContext context)
        {
            _context = context;
        }

        private static CategoryDto MapToDto(CategoryDto categoryDto)
        {
            return new CategoryDto(categoryDto.Name, categoryDto.Description);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _context.Categories
                .AsNoTracking().AsEnumerable()
                .Select(MapToDto)
                .ToList();
        }

        public CategoryDto? GetById(int id)
        {
            return _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == id).AsEnumerable()
                .Select(MapToDto)
                .SingleOrDefault();
        }

        public CategoryDto Create(CategoryDto newCategoryDto)
        {
            // Opret en ny instans af Category-klassen og tildel dens egenskaber værdierne fra CategoryDto-objektet
            var newCategory = new CategoryDto(newCategoryDto.Name, newCategoryDto.Description);

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            newCategoryDto.Id = newCategory.Id;
            newCategoryDto.Products = new List<ProductDto>();

            return newCategoryDto;
        }

        public void Update(int id, CategoryDto updatedCategoryDto)
        {
            var category = _context.Categories
                .Include(c => c.Products)
                .SingleOrDefault(c => c.Id == id);

            if (category == null) return;
            // Update scalar properties
            category.Name = updatedCategoryDto.Name;
            category.Description = updatedCategoryDto.Description;

            // Update Products
            if (updatedCategoryDto.Products.Count > 0)
            {
                category.Products?.Clear();

                var products = _context.Products
                    .Where(p => updatedCategoryDto.Products.Any(cp => cp.Id == p.Id)).ToList();

                foreach (var product in products)
                {
                    category.Products?.Add(product);
                }
            }

            _context.SaveChanges();
        }

        public void Delete(CategoryDto? categoryToDelete)
        {
            if (categoryToDelete == null) return;
            var category = _context.Categories.Find(categoryToDelete.Id);
            if (category is null) return;
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}