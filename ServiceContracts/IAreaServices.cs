﻿using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IAreaServices
	{
		/// <summary>
		/// Adding the new Area in to the Area table
		/// </summary>
		/// <param name="areaAddRequest">The area to add</param>
		/// <returns>AreaResonse obj base on the area adding</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>"
		Task<AreaResponse> AddArea(AreaAddRequest? areaAddRequest);

		/// <summary>
		/// Get All the Area in Area table
		/// </summary>
		/// <returns>A list of Area obj as AreaResponse</returns>
		Task<List<AreaResponse>> GetAllArea();

		/// <summary>
		/// Get Area by Id
		/// </summary>
		/// <param name="id">Area ID</param>
		/// <returns>The matching Area</returns>
		Task<AreaResponse?> GetAreaById(int? id);
	}
}