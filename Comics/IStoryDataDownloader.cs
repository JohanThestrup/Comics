namespace Comics;
public interface IStoryDataDownloader
{
	Task Start();
	Task<StoryData> GetStoryData();
	Task<CharacterData> GetCharacterData(string resourceURI);
	Task<HttpResponseMessage> GetCharacterThumbnail(string thumbnailURL);
}