using Api.DTO;
using Api.Models;
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

        public IEnumerable<CategoryDto> GetAll()
        {
            return _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ProductIds = c.Products.Select(p => p.Id).ToList()
                })
                .ToList();
        }

        public CategoryDto GetById(int id)
        {
            return _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ProductIds = c.Products.Select(p => p.Id).ToList()
                })
                .SingleOrDefault();
        }

        public CategoryDto Create(CategoryDto newCategoryDto)
        {
            var newCategory = new Category
            {
                Name = newCategoryDto.Name,
                Description = newCategoryDto.Description
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            newCategoryDto.Id = newCategory.Id;
            newCategoryDto.ProductIds = new List<int>();

            return newCategoryDto;
        }

        public void Update(int id, CategoryDto updatedCategory)
        {
            var category = _context.Categories
                .Include(p => p.Products)
                .SingleOrDefault(c => c.Id == id);

            if (category != null)
            {
                // Update scalar properties
                category.Name = updatedCategory.Name;
                category.Description = updatedCategory.Description;

                // Update Products
                if (updatedCategory.ProductIds != null && updatedCategory.ProductIds.Count > 0)
                {
                    category.Products.Clear();

                    var products = _context.Products
                        .Where(p => updatedCategory.ProductIds.Contains(p.Id))
                        .ToList();

                    foreach (var product in products)
                    {
                        category.Products.Add(product);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void Delete(CategoryDto categoryToDelete)
        {
            var category = _context.Categories.Find(categoryToDelete.Id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}