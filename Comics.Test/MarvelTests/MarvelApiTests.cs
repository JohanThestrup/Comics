using AutoFixture.Xunit2;
using Comics.Marvel;
using Comics.Marvel.Models;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using System.Text.Json;

namespace Comics.Tests;

public class MarvelApiTests
{
	private readonly IOptions<MarvelConfig> _configuration;
	public MarvelApiTests()
	{
		_configuration = Substitute.For<IOptions<MarvelConfig>>();
		_configuration.Value.Returns(new MarvelConfig
		{
			PrivateKey = "thePrivateKey",
			PublicKey = "thePublicKey",
			ImageVariant = "theImageVariant"
		});
	}

	[Theory, AutoData]
	public async Task GetStory_ShouldReturnStoryDataWrapper(StoryDataWrapper storyDataWrapper)
	{
		// Arrange
		string attributionText = storyDataWrapper.AttributionText;
		var testJson = JsonSerializer.Serialize(storyDataWrapper);

		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(testJson)
		};

		var testHttpMessageHandler = new TestHttpMessageHandler(response);
		var httpClient = new HttpClient(testHttpMessageHandler);
		var marvelApi = new MarvelApi(httpClient, _configuration);

		// Act
		var result = await marvelApi.GetStory();

		// Assert
		result.Should().BeOfType<StoryDataWrapper>();
		result.AttributionText.Should().Be(attributionText);
	}
	[Theory, AutoData]
	public async Task GetCharacter_ShouldReturnCharacterDataWrapper(CharacterDataWrapper characterDataWrapper)
	{
		// Arrange
		string attributionText = characterDataWrapper.AttributionText;
		var testJson = JsonSerializer.Serialize(characterDataWrapper);

		var response = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(testJson)
		};

		var testHttpMessageHandler = new TestHttpMessageHandler(response);
		var httpClient = new HttpClient(testHttpMessageHandler);
		var marvelApi = new MarvelApi(httpClient, _configuration);

		// Act
		var result = await marvelApi.GetCharacter("https://www.characterURI.com");

		// Assert
		result.Should().BeOfType<CharacterDataWrapper>();
		result.AttributionText.Should().Be(attributionText);
	}
	[Fact]
	public async Task GetImage_ShouldReturnHttpResponseMessage()
	{
		// Arrange
		var response = new HttpResponseMessage(HttpStatusCode.OK);

		var testHttpMessageHandler = new TestHttpMessageHandler(response);
		var httpClient = new HttpClient(testHttpMessageHandler);
		var marvelApi = new MarvelApi(httpClient, _configuration);

		// Act
		var result = await marvelApi.GetImage("https://www.thumbnailURI.com");

		// Assert
		result.StatusCode.Should().Be(HttpStatusCode.OK);
		result.Should().BeOfType<HttpResponseMessage>();
	}
}