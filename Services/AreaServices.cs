using System;
using Entities.Models;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helper;

namespace Services
{
	public class AreaServices : IAreaServices
	{
		// private field
		private readonly IAreaRepositories _areaRepositories;

		// constructor
		public AreaServices(IAreaRepositories areaRepositories)
		{
			_areaRepositories = areaRepositories;
		}

		public async Task<AreaResponse> AddArea(AreaAddRequest? areaAddRequest)
		{
			ArgumentNullException.ThrowIfNull(areaAddRequest);

			// Check duplicate AreaName
			var areaExist = await _areaRepositories.GetAreaByName(areaAddRequest.AreaName);

			if(areaExist != null)
			{
				throw new ArgumentException("The AreaName is exist!");
			}

			ValidationHelper.ModelValidation(areaAddRequest);

			Area area = areaAddRequest.MapToArea();

			await _areaRepositories.Add(area);

			return area.ToAreaResponse();

		}

		public async Task<bool> DeleteArea(int? id)
		{
			if(id == null) throw new ArgumentNullException(nameof(id));

			Area? area = await _areaRepositories.GetAreaById(id);

			if(area == null)
			{
				return false;
			}

			await _areaRepositories.DeleteArea(id.Value);

			return true;
		}

		public async Task<List<AreaResponse>> GetAllArea()
		{
			var listArea = await _areaRepositories.GetAllArea();
			var listAreaResponse = listArea.Select(area => area.ToAreaResponse()).ToList();
			return listAreaResponse;
		}

		public async Task<AreaResponse?> GetAreaById(int? id)
		{
			if (id == null) return null;

			var area = await _areaRepositories.GetAreaById(id);

			if (area == null) return null;

			return area.ToAreaResponse();
		}

		public async Task<AreaResponse> UpdateArea(AreaUpdateRequest? areaUpdateRequest)
		{
			if(areaUpdateRequest == null) throw new ArgumentNullException(nameof(areaUpdateRequest));

			ValidationHelper.ModelValidation(areaUpdateRequest);

			Area? matchingArea = await _areaRepositories.GetAreaById(areaUpdateRequest.AreaId);

			if(matchingArea == null) throw new ArgumentException("Given Area id doesn't exsit");

			matchingArea.AreaName = areaUpdateRequest.AreaName;
			matchingArea.IsDelete = areaUpdateRequest.IsDelete;	

			await _areaRepositories.UpdateArea(matchingArea);

			return matchingArea.ToAreaResponse();
		}
	}
}
