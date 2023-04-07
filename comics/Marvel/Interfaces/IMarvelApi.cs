using comics.Marvel.Models;
namespace comics.Marvel.Interfaces;
public interface IMarvelApi
{
	Task<StoryDataWrapper> GetStory();
	Task<CharacterDataWrapper> GetCharacter(string characterURI);
	Task<HttpResponseMessage> GetImage(string thumbnailURL);
	string CreateHash(long ts);
	string BuildThumbnailURL(string path, string extension);
}