using comics.Marvel;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Comics.Tests;

public class MarvelApiTests
{
	private readonly MarvelApi _marvelApi;
	private readonly HttpClient _httpClient;
	private readonly IOptions<MarvelConfig> _configuration;
	public MarvelApiTests()
	{
		_httpClient = Substitute.For<HttpClient>();
		_configuration = Substitute.For<IOptions<MarvelConfig>>();
		_configuration.Value.Returns(new MarvelConfig
		{
			PrivateKey = "thePrivateKey",
			PublicKey = "thePublicKey",
			ImageVariant = "theImageVariant"
		});
		_marvelApi = new MarvelApi(_httpClient, _configuration);
	}
	[Theory]
	[InlineData("path1", "extension1")]
	[InlineData("path2", "extension2")]
	public void BuildThumbnailURL_Returns_CorrectURL(string path, string extension)
	{
		// Arrange
		// Act
		var result = _marvelApi.BuildThumbnailURL(path, extension);

		// Asssert
		result.Should().Be($"{path}/{_configuration.Value.ImageVariant}.{extension}");
	}

}

