using System.IO.Abstractions;

namespace Comics;
public class FileHandler : IFileHandler
{
	private readonly IFileSystem _fileSystem;
	public FileHandler(IFileSystem fileSystem)
	{
		_fileSystem = fileSystem;
	}

	public string CreateDirectory(string root, string storyTitle)
	{
		var dir = Path.Combine(root, storyTitle);
		if (!_fileSystem.Directory.Exists(dir))
		{
			_fileSystem.Directory.CreateDirectory(dir);
		}

		return dir;
	}

	public void SaveTxtFile(string path, string[] textContent)
	{
		if (_fileSystem.File.Exists(path))
		{
			_fileSystem.File.Delete(path);
		}

		using StreamWriter sw = _fileSystem.File.CreateText(path);
		foreach (string line in textContent)
		{
			sw.WriteLine(line);
		}
	}

	public void SaveImage(HttpResponseMessage res, string path)
	{
		if (_fileSystem.File.Exists(path))
		{
			_fileSystem.File.Delete(path);
		}

		_fileSystem.File.WriteAllBytes(path, res.Content.ReadAsByteArrayAsync().Result);
	}
}
public interface IFileHandler
{
	string CreateDirectory(string root, string storyTitle);
	void SaveTxtFile(string fileName, string[] textContent);
	void SaveImage(HttpResponseMessage res, string path);
};