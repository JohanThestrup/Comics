using AutoFixture.Xunit2;
using Comics.Marvel;
using Comics.Marvel.Models;
using NSubstitute;

namespace Comics.Tests;
public class StoryDataDownloaderTest
{
	private readonly StoryDataDownloader _storyDataDownloader;
	private readonly IMarvelApi _marvelApi;
	private readonly IFileHandler _fileHandler;
	public StoryDataDownloaderTest()
	{
		_marvelApi = Substitute.For<IMarvelApi>();
		_fileHandler = Substitute.For<IFileHandler>();
		// SUT
		_storyDataDownloader = new StoryDataDownloader(_marvelApi, _fileHandler);
	}

	[Theory, AutoData]
	public async Task GetStoryData_Returns_StoryData(StoryDataWrapper storyDataWrapper)
	{
		// Arrange
		_marvelApi.GetStory().Returns(storyDataWrapper);
		// Act
		var result = await _storyDataDownloader.GetStoryData();
		// Assert
		result.Should().BeOfType<StoryData>();
	}

	[Theory, AutoData]
	public async Task GetCharacterData_Returns_CharacterData(CharacterDataWrapper characterDataWrapper)
	{
		// Arrange
		_marvelApi.GetCharacter("uri").Returns(characterDataWrapper);
		// Act
		var result = await _storyDataDownloader.GetCharacterData("uri");
		// Assert
		result.Should().BeOfType<CharacterData>();
	}
}
