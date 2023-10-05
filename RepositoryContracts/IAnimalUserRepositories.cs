﻿

using Entities.Models;

namespace RepositoryContracts
{
	public interface IAnimalUserRepositories
	{
		/// <summary>
		/// Adds a AnimalUser object to the data store
		/// </summary>
		/// <param name="animalUser">The user to add</param>
		/// <returns>AnimalUser obj after adding</returns>
		Task<AnimalUser> Add(AnimalUser animalUser);

		/// <summary>
		/// Get the relationship between animal and user
		/// </summary>
		/// <param name="animalId">The animal id</param>
		/// <param name="userId">The zoo trainer id</param>
		/// <returns>Return the matching AnimalUser obj</returns>
		Task<AnimalUser?> GetAnimalUserRelationship(long? animalId, long? userId);
	}
}
