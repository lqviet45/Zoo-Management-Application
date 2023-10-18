using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class NewsCategoriesRepositories : INewsCategoriesRepositories
	{
		// private field
		private readonly ApplicationDbContext _dbContext;

		// constructor
		public NewsCategoriesRepositories(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<NewsCategories> Add(NewsCategories newsCategories)
		{
			_dbContext.NewsCategories?.Add(newsCategories);
			await _dbContext.SaveChangesAsync();
			return newsCategories;
		}

		public async Task<bool> DeleteCategory(int categoryId)
		{
			var deleteCate = _dbContext.NewsCategories?.Where(cate => cate.CategoryId == categoryId)
							.FirstOrDefault();

			if (deleteCate is null)
			{
				return false;
			}

			_dbContext.NewsCategories?.Remove(deleteCate);

			int rowDeleted = await _dbContext.SaveChangesAsync();

			return rowDeleted > 0;
		}

		public async Task<List<NewsCategories>> GetAllCategories()
		{
			var listCate = await _dbContext.NewsCategories.ToListAsync();
			return listCate;
		}

		public async Task<NewsCategories?> GetCategoryById(int cateId)
		{
			var cate = await _dbContext.NewsCategories.Where(cate => cate.CategoryId == cateId)
						.FirstOrDefaultAsync();
			return cate;
		}

		public async Task<NewsCategories?> GetCategoryByName(string name)
		{
			var cate = await _dbContext.NewsCategories.Where(cate => cate.CategoryName == name)
						.FirstOrDefaultAsync();

			return cate;
		}

		public async Task<NewsCategories> UpdateCategory(NewsCategories newsCategories)
		{
			NewsCategories? updateCate = await _dbContext.NewsCategories
							.Where(cate => cate.CategoryId == newsCategories.CategoryId)
							.FirstOrDefaultAsync();

			if (updateCate == null)
			{
				return newsCategories;
			}

			updateCate.CategoryName = newsCategories.CategoryName;
			 await _dbContext.SaveChangesAsync();

			return updateCate;

		}
	}
}
