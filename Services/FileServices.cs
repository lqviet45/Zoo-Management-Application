using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceContracts;


namespace Services
{
	public class FileServices : IFileServices
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

        public FileServices(IWebHostEnvironment env)
        {
            this._webHostEnvironment = env;
        }
        public bool DeleteImage(string imageFileName)
		{
			try
			{
				var wwwPath = _webHostEnvironment.WebRootPath;
				var path = Path.Combine(wwwPath, "images\\", imageFileName);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
					return true;
				}
				return false;
			} 
			catch(Exception)
			{
				return false;
			}
		}

		public Tuple<int, string> SaveImage(IFormFile imageFile)
		{
			try
			{
				string path = _webHostEnvironment.WebRootPath + "\\images\\";
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				// Check the allowed extenstions
				var ext = Path.GetExtension(imageFile.FileName);
				var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
				if (!allowedExtensions.Contains(ext))
				{
					string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
					return new Tuple<int, string>(0, msg);
				}

				string uniqueString = Guid.NewGuid().ToString();
				// we are trying to create a unique filename here
				var newFileName = uniqueString + ext;
				var fileWithPath = Path.Combine(path, newFileName);
				var stream = new FileStream(fileWithPath, FileMode.Create);
				imageFile.CopyTo(stream);
				stream.Close();
				return new Tuple<int, string>(1, fileWithPath);

			}
			catch(Exception)
			{
				return new Tuple<int, string>(0, "Error has occured");
			}
		}
	}
}
