using comics.Marvel;
using comics.Marvel.Interfaces;
using comics.Marvel.Models;

namespace comics;
public class StoryDataDownloader : IStoryDataDownloader
{
	private readonly IMarvelApi _marvelApi;
	private readonly IFileHandler _fileHandler;

	public StoryDataDownloader(IMarvelApi marvelApi, IFileHandler fileHandler)
	{
		_marvelApi = marvelApi;
		_fileHandler = fileHandler;
	}

	public async Task Start()
	{
		// Get the required story data and create directory
		var (StoryTitle, TextFileContent, Characters) = await GetStoryData();
		var folderPath = _fileHandler.CreateDirectory(StoryTitle);

		// Create and save text file
		_fileHandler.SaveTxtFile(Path.Combine(folderPath, StoryTitle + ".txt"), TextFileContent);

		// Get image for all characters
		foreach (var character in Characters)
		{
			var (CharacterName, Path, Extension) = await GetCharacterData(character.ResourceURI);
			if (!Path.Contains("image_not_available"))
			{
				//var requestURL = $"{thumbnailPath}/{_configuration.ImageVariant}.{thumbnailExtension}";
				var thumbnailURL = _marvelApi.BuildThumbnailURL(Path, Extension);
				var image = await GetCharacterThumbnail(thumbnailURL);
				// Save the image/images
				await _fileHandler.SaveImage(image, folderPath + "/" + CharacterName + "." + Extension);
			}
		}
	}

	public async Task<StoryData> GetStoryData()
	{
		StoryDataWrapper storyResponse = await _marvelApi.GetStory();
		Story story = storyResponse.Data.Results.FirstOrDefault();

		string description = story.Description != "" ? story.Description : $"The story {story.Title} does not contain a description.";

		return new StoryData(story.Title, new string[] {description, storyResponse.AttributionText}, story.Characters.Items);
	}

	public async Task<CharacterData> GetCharacterData(string resourceURI)
	{
		CharacterDataWrapper characterResponse = await _marvelApi.GetCharacter(resourceURI);
		Character character = characterResponse.Data.Results.FirstOrDefault();

		return new CharacterData(character.Name, character.Thumbnail.Path, character.Thumbnail.Extension);
	}

	public async Task<HttpResponseMessage> GetCharacterThumbnail(string thumbnailURL)
	{
		return await _marvelApi.GetImage(thumbnailURL);
	}
}
public record StoryData(string StoryTitle, string[] TextFileContent, CharacterSummary[] Characters);
public record CharacterData(string CharacterName, string Path, string Extension);