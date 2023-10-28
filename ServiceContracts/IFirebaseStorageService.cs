using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	public interface IFirebaseStorageService
	{
		Task<string> UploadFile(string name, IFormFile file);
	}
}
