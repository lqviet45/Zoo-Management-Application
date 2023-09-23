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

			ValidationHelper.ModelValidation(areaAddRequest);

			Area area = areaAddRequest.MapToArea();

			await _areaRepositories.Add(area);

			return area.ToAreaResponse();

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
	}
}
