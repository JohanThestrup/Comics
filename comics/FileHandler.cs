namespace comics
{
	public class FileHandler
	{
		public string root = Path.GetFullPath("../../../");

		public string CreateDirectory(string storyTitle)
		{
			var dir = Path.Combine(root, storyTitle);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			return dir;
		}

		public void SaveTxtFile(string fileName, string[] textContent)
		{
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}

			using (StreamWriter sw = File.CreateText(fileName))
			{
				foreach (string line in textContent)
				{
					sw.WriteLine(line);
				}
			}
		}

		public async Task SaveImage(HttpResponseMessage res, string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}

			using (var fs = new FileStream(path, FileMode.CreateNew))
			{
				await res.Content.CopyToAsync(fs);
			}
		}
	}
}
