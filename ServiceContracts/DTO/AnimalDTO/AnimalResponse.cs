using Entities.Models;
using ServiceContracts.DTO.SpeciesDTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.AnimalDTO
{
	/// <summary>
	/// Represents DTO class that is used as return type of most methods of Animal service
	/// </summary>
	public class AnimalResponse
	{
		public long AnimalId { get; set; }

		[Required(ErrorMessage = "AnimalSpecies Can not be blank!")]
		public int SpeciesId { get; set; }

		[Required(ErrorMessage = "AnimalName Can not be blank!")]
		public string? AnimalName { get; set; }

		[Required(ErrorMessage = "DateArrive can not be blank!")]
		public DateTime DateArrive { get; set; }

		[Required(ErrorMessage = "Status can not be blank!")]
		public string? Status { get; set; }

		[Required(ErrorMessage = "IsDelete can not be blank!")]
		public SpeciesResponse? Species { get; set; } 

	}

    public static class AnimalExtension
    {
		/// <summary>
		/// A method to Convert Animal To AnimalResponse
		/// </summary>
		/// <param name="animal">Animal to convert</param>
		/// <returns>AnimalResponse object base on the user</returns>
		public static AnimalResponse ToAnimalResponse(this Animal animal)
		{
			return new AnimalResponse()
			{
				AnimalId = animal.AnimalId,
				AnimalName = animal.AnimalName,
				DateArrive = animal.DateArrive,
				Status = animal.Status,
				SpeciesId = animal.SpeciesId,
				Species = animal.Species.ToSpeciesResponse()
			};
		}
    }

}
