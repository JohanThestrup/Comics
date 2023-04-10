using System.IO.Abstractions.TestingHelpers;
using System.Net;

namespace Comics.Tests;
public class FileHandlerTests
{
	private readonly string rootPath = @"C:\TestFileSystem";
	private readonly MockFileSystem _fileSystem;
	private readonly FileHandler _fileHandler;

	public FileHandlerTests()
	{
		_fileSystem = new MockFileSystem();
		// SUT
		_fileHandler = new FileHandler(_fileSystem);
	}

	[Fact]
	public void CreateDirectory_ShouldCreateDirectoryAndReturnPath()
	{
		// Arrange
		string storyTitle = "Hulk";

		// Act
		var result = _fileHandler.CreateDirectory(rootPath, storyTitle);

		// Assert
		var expectedPath = Path.Combine(rootPath, storyTitle);
		_fileSystem.Directory.Exists(expectedPath).Should().BeTrue();
		result.Should().Be(expectedPath);
	}
	[Fact]
	public void SaveTxtFile_ShouldSaveTextFile()
	{
		// Arrange
		string[] content = new[] { "Line1", "Line2" };
		string textPath = "text.txt";

		// Act
		_fileHandler.SaveTxtFile(textPath, content);

		// Assert
		_fileSystem.File.Exists(textPath).Should().BeTrue();
		_fileSystem.File.ReadAllText(textPath).Trim().Should().Be(string.Join("\r\n", content));
	}

	[Fact]
	public void SaveImage_ShouldSaveImage()
	{
		// Arrange
		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new ByteArrayContent(new byte[] { 0x1, 0x2, 0x3 })
		};
		string imagePath = "hulk.jpg";

		// Act
		_fileHandler.SaveImage(response, imagePath);

		// Assert
		_fileSystem.File.Exists(imagePath).Should().BeTrue();
		_fileSystem.File.ReadAllBytes(imagePath).Should().Equal(new byte[] { 0x1, 0x2, 0x3 });
	}
}
