using comics.MarvelApi;
using comics.MarvelApi.Models;

namespace comics
{
	public class StoryDataDownloader
	{
		private Marvel _marvel = new Marvel();
		private FileHandler _fileHandler = new FileHandler();
		public string StoryTitle;
		public string[] TextFileContent;
		public CharacterSummary[] Characters;

		public async Task Start()
		{
			await GetStoryData();
			var folderPath = _fileHandler.CreateDirectory(StoryTitle);
			_fileHandler.SaveTxtFile(folderPath + "/" + StoryTitle + ".txt", TextFileContent);

			foreach (var character in Characters)
			{
				CharacterDataWrapper characterResponse = await _marvel.GetCharacter(character.ResourceURI);
				Character characterResult = characterResponse.Data.Results.FirstOrDefault();
				var(path, extension) = characterResult.Thumbnail;
				string characterName = characterResult.Name;

				if (!path.Contains("image_not_available"))
				{
					var image = await GetCharacterThumbnail(path, extension, characterName);
					await _fileHandler.SaveImage(image, folderPath + "/" + characterName + "." + extension);
				}
			}
		}

		public async Task GetStoryData()
		{
			StoryDataWrapper storyResponse = await _marvel.GetStory();

			Story story = storyResponse.Data.Results.FirstOrDefault();
			TextFileContent = new string[] { story.Description, storyResponse.AttributionText };
			StoryTitle = story.Title;
			Characters = story.Characters.Items;
		}

		public async Task<HttpResponseMessage> GetCharacterThumbnail(string thumbnailPath, string thumbnailExtension, string characterName)
		{
			return await _marvel.GetImage(thumbnailPath, thumbnailExtension, characterName);
		}
	}
}
