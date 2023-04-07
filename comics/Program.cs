using comics;
using comics.Marvel;
using comics.Marvel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<IMarvelApi, MarvelApi>();
serviceCollection.AddHttpClient<IMarvelApi, MarvelApi>();
serviceCollection.AddScoped<IFileHandler, FileHandler>();
serviceCollection.AddScoped<IStoryDataDownloader, StoryDataDownloader>();
var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
	.AddJsonFile("appsettings.json")
	.Build();
serviceCollection.Configure<MarvelConfig>(configuration.GetSection("MarvelApi"));
serviceCollection.AddScoped<MarvelConfig>();
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
var storyDataDownloader = serviceProvider.GetRequiredService<IStoryDataDownloader>();
await storyDataDownloader.Start();