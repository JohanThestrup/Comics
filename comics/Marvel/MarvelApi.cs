using Comics.Marvel.Interfaces;
using Comics.Marvel.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Comics.Marvel;
public class MarvelApi : IMarvelApi
{
	private readonly MarvelConfig _configuration;
	private readonly HttpClient _httpClient;

	public MarvelApi(HttpClient httpClient, IOptions<MarvelConfig> configuration)
	{
		_httpClient = httpClient;
		_configuration = configuration.Value;
	}
	public async Task<StoryDataWrapper> GetStory()
	{
		var ts = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
		var hash = CreateHash(ts);
		var requestURL = $"https://gateway.marvel.com/v1/public/stories/702?ts={ts}&apikey={_configuration.PublicKey}&hash={hash}";

		var url = new Uri(requestURL);
		HttpResponseMessage res = await _httpClient.GetAsync(url);
		res.EnsureSuccessStatusCode();

		var responseBody = await res.Content.ReadAsStringAsync();

		StoryDataWrapper sdw = JsonConvert.DeserializeObject<StoryDataWrapper>(responseBody);
		return sdw;
	}
	public async Task<CharacterDataWrapper> GetCharacter(string characterURI)
	{
		var ts = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
		var hash = CreateHash(ts);
		var requestURL = $"{characterURI}?ts={ts}&apikey={_configuration.PublicKey}&hash={hash}";

		var url = new Uri(requestURL);
		HttpResponseMessage res = await _httpClient.GetAsync(url);
		res.EnsureSuccessStatusCode();

		var responseBody = await res.Content.ReadAsStringAsync();
		CharacterDataWrapper cdw = JsonConvert.DeserializeObject<CharacterDataWrapper>(responseBody);
		return cdw;
	}
	public async Task<HttpResponseMessage> GetImage(string thumbnailURL)
	{
		var url = new Uri(thumbnailURL);
		HttpResponseMessage res = await _httpClient.GetAsync(url);
		res.EnsureSuccessStatusCode();

		return res;
	}

	public string CreateHash(long ts)
	{
		var stringToHash = $"{ts}{_configuration.PrivateKey}{_configuration.PublicKey}";
		var md5 = MD5.Create();
		var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

		var sb = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++)
		{
			sb.Append(bytes[i].ToString("x2"));
		}
		return sb.ToString();
	}

	public string BuildThumbnailURL(string path, string extension)
	{
		return $"{path}/{ _configuration.ImageVariant}.{extension}";
	}
}
