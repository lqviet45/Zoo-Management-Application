using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.SpeciesDTO;
using Services.Helper;

namespace Services
{
    public class SpeciesServices : ISpeciesServices
	{
		// private field
		private readonly ISpeciesRepositories _speciesRepositories;
		private readonly IFirebaseStorageService _firebaseStorageService;
		private const string _folder = "animal";

		// constructor
		public SpeciesServices(ISpeciesRepositories speciesRepositories,IFirebaseStorageService firebaseStorageService)
		{
			_speciesRepositories = speciesRepositories;
			_firebaseStorageService = firebaseStorageService;
		}
		public async Task<SpeciesResponse> AddSpecies(SpeciesAddRequest? speciesAddRequest)
		{
			ArgumentNullException.ThrowIfNull(speciesAddRequest);

			// Check Duplicate SpeciesName
			var speciesExist = await _speciesRepositories.GetSpeciesByName(speciesAddRequest.SpeciesName);
			if (speciesExist is not null)
			{
				throw new ArgumentException("The SpeciesName is exist!");
			}

			ValidationHelper.ModelValidation(speciesAddRequest);

			Species species = speciesAddRequest.MapToSpecies();

			if(speciesAddRequest.ImageFile != null)
			{
				var imageUri = await _firebaseStorageService.UploadFile(speciesAddRequest.SpeciesName, speciesAddRequest.ImageFile, _folder);
				species.Image = imageUri;
			}

			await _speciesRepositories.Add(species);

			return species.ToSpeciesResponse();
		}

		public async Task<bool> DeleteSpecies(int? id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));

			Species? species = await _speciesRepositories.GetSpeciesById(id.Value);

			if (species == null)
			{
				return false;
			}

			await _speciesRepositories.Delete(id.Value);

			return true;
		}

		public async Task<List<SpeciesResponse>> GetAllSpecies()
		{
			var listSpecies = await _speciesRepositories.GetAllSpecies();

			return listSpecies.Select(temp => temp.ToSpeciesResponse()).ToList();
		}

		public async Task<List<SpeciesResponse>> GetFilteredSpecies(string searchBy, string? searchString)
		{
			if(string.IsNullOrEmpty(searchString)) searchString = string.Empty;

			List<Species> species = searchBy switch
			{
				nameof(SpeciesResponse.SpeciesName) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.SpeciesName.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.Family) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.Family.Contains(searchString) && temp.IsDeleted == false),
				
				nameof(SpeciesResponse.Information) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.Information.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.Characteristic) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.Characteristic.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.Allocation) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.Allocation.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.Ecological) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
						temp.Ecological.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.Diet) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
									temp.Diet.Contains(searchString) && temp.IsDeleted == false),

				nameof(SpeciesResponse.BreedingAndReproduction) =>
				await _speciesRepositories.GetFilteredSpecies(temp => 
					temp.BreedingAndReproduction.Contains(searchString) && temp.IsDeleted == false),

				_ => await _speciesRepositories.GetAllSpecies()
			};

			return species.Select(temp => temp.ToSpeciesResponse()).ToList();

		}

		public async Task<SpeciesResponse?> GetSpeciesById(int? id)
		{
			if(id == null) return null;

			var species = await _speciesRepositories.GetSpeciesById(id.Value);

			if (species == null)
			{
				return null;
			}

			return species.ToSpeciesResponse();
		}

		public async Task<SpeciesResponse> UpdateSpecies(SpeciesUpdateRequest? speciesUpdateRequest)
		{
			if(speciesUpdateRequest == null) throw new ArgumentNullException(nameof(speciesUpdateRequest));

			ValidationHelper.ModelValidation(speciesUpdateRequest);

			Species? matchingSpecies = await _speciesRepositories.GetSpeciesById(speciesUpdateRequest.SpeciesId);

			if (matchingSpecies == null)
			{
				throw new ArgumentException("The SpeciesId is not exist!");
			}

			matchingSpecies.SpeciesName = speciesUpdateRequest.SpeciesName;
			matchingSpecies.Family = speciesUpdateRequest.Family;
			matchingSpecies.Information = speciesUpdateRequest.Information;
			matchingSpecies.Characteristic = speciesUpdateRequest.Characteristic;
			matchingSpecies.Ecological = speciesUpdateRequest.Ecological;
			matchingSpecies.Allocation = speciesUpdateRequest.Allocation;
			matchingSpecies.Diet = speciesUpdateRequest.Diet;
			matchingSpecies.BreedingAndReproduction = speciesUpdateRequest.BreedingAndReproduction;
			matchingSpecies.IsDeleted = speciesUpdateRequest.IsDeleted;
			
			if(speciesUpdateRequest.ImageFile != null)
			{

				var imageUri = await _firebaseStorageService.UploadFile(speciesUpdateRequest.SpeciesName, speciesUpdateRequest.ImageFile, _folder);
				matchingSpecies.Image = imageUri;

			}

			await _speciesRepositories.Update(matchingSpecies);

			return matchingSpecies.ToSpeciesResponse();
		}
	}
}
