using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class FirebaseStorageService : IFirebaseStorageService
	{
		private readonly StorageClient _storageClient;
		private const string BucketName = "zoo-management-application.appspot.com";

        public FirebaseStorageService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<string> UploadFile(string name, IFormFile file)
		{
			var randomGuid = Guid.NewGuid();

			using var stream = new MemoryStream();
			await file.CopyToAsync(stream);

			var image = await _storageClient.UploadObjectAsync(BucketName,
				$"images/ticket/{name}-{randomGuid}", file.ContentType, stream);

			var photoUri = image.MediaLink;

			return photoUri;
		}
	}
}
