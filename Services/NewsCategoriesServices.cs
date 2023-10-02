using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.NewsCategoriesDTO;
using Services.Helper;

namespace Services
{
	public class NewsCategoriesServices : INewsCategoriesServices
	{
		// private fields
		private readonly INewsCategoriesRepositories _newsCategoriesRepositories;

		// constructor
		public NewsCategoriesServices(INewsCategoriesRepositories newsCategoriesRepositories)
		{
			_newsCategoriesRepositories = newsCategoriesRepositories;
		}

		public async Task<NewsCategoryResponse> AddNewsCategories(NewsCategoryAddRequest? newsCategoriesAddRequest)
		{
			ArgumentNullException.ThrowIfNull(newsCategoriesAddRequest);

			// Check duplicate CategoryName
			var newsCategoriesExist = await _newsCategoriesRepositories.GetCategoryByName(newsCategoriesAddRequest.CategoryName);

			if(newsCategoriesExist != null)
			{
				throw new ArgumentException("The CategoryName is exist!");
			}

			ValidationHelper.ModelValidation(newsCategoriesAddRequest);

			NewsCategories newsCategories = newsCategoriesAddRequest.MapToNewsCategories();

			await _newsCategoriesRepositories.Add(newsCategories);

			return newsCategories.ToNewsCategoryResponse();
		}

		public async Task<bool> DeleteCategory(int categoryId)
		{
			
			var categoryDelete = await GetCategoryById(categoryId);

			if(categoryDelete == null)
			{
				return false;
			}

			await _newsCategoriesRepositories.DeleteCategory(categoryId);

			return true;
		}

		public async Task<List<NewsCategoryResponse>> GetAllNewsCategories()
		{
			var listCate = await _newsCategoriesRepositories.GetAllCategories();

			var listCateResponse = listCate.Select(cate => cate.ToNewsCategoryResponse()).ToList();

			return listCateResponse;
		}

		public async Task<NewsCategoryResponse?> GetCategoryById(int categoryId)
		{
			var category = await _newsCategoriesRepositories.GetCategoryById(categoryId);

			if(category == null)
			{
				return null;
			}

			return category.ToNewsCategoryResponse();
		}

		public async Task<NewsCategoryResponse> UpdateNewsCategories(NewsCategoryUpdateRequest? newsCategoriesUpdateRequest)
		{
			if(newsCategoriesUpdateRequest == null)
			{
				throw new ArgumentNullException(nameof(newsCategoriesUpdateRequest));
			}

			ValidationHelper.ModelValidation(newsCategoriesUpdateRequest);	

			var updatedNewsCategories = await _newsCategoriesRepositories.GetCategoryById(newsCategoriesUpdateRequest.CategoryId);

			if (updatedNewsCategories == null)
			{
				throw new ArgumentException("The newsCategories is not exist!");
			}

			updatedNewsCategories.CategoryName = newsCategoriesUpdateRequest.CategoryName;

			await _newsCategoriesRepositories.UpdateCategory(updatedNewsCategories);

			return updatedNewsCategories.ToNewsCategoryResponse();
		}
	}
}
