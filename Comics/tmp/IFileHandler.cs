namespace Comics;
public interface IFileHandler
{
	string CreateDirectory(string storyTitle);
	void SaveTxtFile(string fileName, string[] textContent);
	Task SaveImage(HttpResponseMessage res, string path);
}