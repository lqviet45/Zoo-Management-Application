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
		/// <summary>
		/// Uploading the img to FireBase
		/// </summary>
		/// <param name="name">The img name</param>
		/// <param name="file">The file to upload</param>
		/// <returns>A uri of the img upload</returns>
		Task<string> UploadFile(string name, IFormFile file);

		/// <summary>
		/// Uploading the img to FireBase this function allow you to choose where to save image to FireBase
		/// </summary>
		/// <param name="name">The img name</param>
		/// <param name="file">The file to upload</param>
		/// <param name="folderSave">The folder to save the image if the folder is not exist it will auto create</param>
		/// <returns>A uri of the img upload</returns>
		Task<string> UploadFile(string name, IFormFile file, string folderSave);
	}
}
