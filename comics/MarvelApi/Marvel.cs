using comics.MarvelApi.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace comics.MarvelApi
{
	public class Marvel
	{
		private string baseURL = "https://gateway.marvel.com";
		private string privateKey = "6508a713798d60b4506b0d0af4fe051eb6e5a046";
		private string publicKey = "6bef29cf2e78f3703727da0a7fddf624";
		HttpClient httpClient = new HttpClient();

		public async Task<StoryDataWrapper> GetStory()
		{
			var ts = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

			var stringToHash = String.Format("{0}{1}{2}", ts, privateKey, publicKey);
			var hash = CreateHash(stringToHash);
			var requestURL = String.Format(baseURL + "/v1/public/stories/702?ts={0}&apikey={1}&hash={2}", ts, publicKey, hash);

			var url = new Uri(requestURL);
			HttpResponseMessage res = await httpClient.GetAsync(url);

			var responseBody = await res.Content.ReadAsStringAsync();

			StoryDataWrapper sdw = JsonConvert.DeserializeObject<StoryDataWrapper>(responseBody);
			return sdw;
		}
		public async Task<CharacterDataWrapper> GetCharacter(string characterUri)
		{
			var ts = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

			var stringToHash = String.Format("{0}{1}{2}", ts, privateKey, publicKey);
			var hash = CreateHash(stringToHash);
			var requestURL = String.Format(characterUri + "?ts={0}&apikey={1}&hash={2}", ts, publicKey, hash);

			var url = new Uri(requestURL);
			HttpResponseMessage res = await httpClient.GetAsync(url);

			var responseBody = await res.Content.ReadAsStringAsync();
			CharacterDataWrapper cdw = JsonConvert.DeserializeObject<CharacterDataWrapper>(responseBody);
			return cdw;
		}
		public async Task<HttpResponseMessage> GetImage(string thumbnailPath, string thumbnailExtension, string characterName)
		{
			var imageVariant = "portrait_small";

			var requestURL = String.Format(thumbnailPath + "/{0}.{1}", imageVariant, thumbnailExtension);

			var url = new Uri(requestURL);

			return await httpClient.GetAsync(url);
		}
		public string CreateHash(string input)
		{
			var md5 = MD5.Create();
			var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

			var sb = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				sb.Append(bytes[i].ToString("x2"));
			}
			return sb.ToString();
		}
	}
}
