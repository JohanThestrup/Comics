using System.IO.Abstractions.TestingHelpers;
using System.Net;

namespace Comics.Tests;
public class FileHandlerTests
{
	private readonly string rootPath = Environment.CurrentDirectory;
	private readonly string imagePath = "hulk.jpg";
	private readonly string textPath = "text.txt";
	[Fact]
	public void CreateDirectory_ShouldCreateDirectoryAndReturnPath()
	{
		// Arrange
		var fileSystem = new MockFileSystem();
		var fileHandler = new FileHandler(fileSystem);
		var storyTitle = "Hulk";

		// Act
		var result = fileHandler.CreateDirectory(rootPath, storyTitle);

		// Assert
		var expectedPath = Path.Combine(rootPath, storyTitle);
		fileSystem.Directory.Exists(expectedPath).Should().BeTrue();
		result.Should().Be(expectedPath);
	}
	[Fact]
	public void SaveTxtFile_ShouldSaveTextFile()
	{
		// Arrange
		var fileSystem = new MockFileSystem();
		var fileHandler = new FileHandler(fileSystem);
		var content = new[] { "Line1", "Line2" };

		// Act
		fileHandler.SaveTxtFile(textPath, content);

		// Assert
		fileSystem.File.Exists(textPath).Should().BeTrue();
		fileSystem.File.ReadAllText(textPath).Trim().Should().Be(string.Join("\r\n", content));
	}

	[Fact]
	public void SaveImage_ShouldSaveImage()
	{
		// Arrange
		var fileSystem = new MockFileSystem();
		fileSystem.AddDirectory(rootPath);
		var fileHandler = new FileHandler(fileSystem);
		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new ByteArrayContent(new byte[] { 0x1, 0x2, 0x3 })
		};

		// Act
		fileHandler.SaveImage(response, imagePath);

		// Assert
		fileSystem.File.Exists(imagePath).Should().BeTrue();
		fileSystem.File.ReadAllBytes(imagePath).Should().Equal(new byte[] { 0x1, 0x2, 0x3 });
	}
}
