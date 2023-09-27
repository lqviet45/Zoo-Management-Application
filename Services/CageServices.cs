using Entities.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AreaDTO;
using ServiceContracts.DTO.CageDTO;
using Services.Helper;

namespace Services
{
	public class CageServices : ICageServices
	{
		// private field
		private readonly ICageRepositories _cageRepositories;

		// constructor
		public CageServices(ICageRepositories cageRepositories)
		{
			_cageRepositories = cageRepositories;
		}
		public async Task<CageResponse> AddCage(CageAddRequest? cageAddRequest)
		{
			ArgumentNullException.ThrowIfNull(cageAddRequest);

			// Check Duplicate CageName
			var cageExist = await _cageRepositories.GetCageByName(cageAddRequest.CageName);
			if (cageExist != null)
			{
				throw new ArgumentException("The CageName is exist!");
			}

			ValidationHelper.ModelValidation(cageAddRequest);

			Cage cage = cageAddRequest.MapToCage();

			await _cageRepositories.Add(cage);

			return cage.ToCageResponse();
		}

		public async Task<bool> DeleteCage(int? id)
		{
			if(id == null) throw new ArgumentNullException(nameof(id));	

			Cage? cage = await _cageRepositories.GetCageById(id);

			if(cage == null)
			{
				return false;
			}

			await _cageRepositories.DeleteCage(id.Value);

			return true;
		}

		public async Task<List<CageResponse>> GetAllCage()
		{
			var listCage = await _cageRepositories.GetAllCage();

			return listCage.Select(temp => temp.ToCageResponse()).ToList();
		}

		public async Task<CageResponse?> GetCageById(int? id)
		{
			if(id == null) return null;

			Cage? cage = await _cageRepositories.GetCageById(id);

			if(cage == null)
			{
				return null;
			}

			return cage.ToCageResponse();
		}

		public async Task<CageResponse> UpdateCage(CageUpdateRequest? cageUpdateRequest)
		{
			if(cageUpdateRequest == null) throw new ArgumentNullException(nameof(cageUpdateRequest));

			ValidationHelper.ModelValidation(cageUpdateRequest);

			Cage? matchingCage = await _cageRepositories.GetCageById(cageUpdateRequest.CageId);

			if(matchingCage == null)
			{
				throw new ArgumentException("Given Cage id doesn't exsit");
			}

			matchingCage.CageName = cageUpdateRequest.CageName;
			matchingCage.IsDelete = cageUpdateRequest.IsDelete;

			await _cageRepositories.UpdateCage(matchingCage);

			return matchingCage.ToCageResponse();

		}
	}
}
