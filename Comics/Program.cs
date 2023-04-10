using Comics;
using Comics.Marvel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<IMarvelApi, MarvelApi>();
serviceCollection.AddHttpClient<IMarvelApi, MarvelApi>();
serviceCollection.AddScoped<IFileSystem, FileSystem>();
serviceCollection.AddScoped<IFileHandler, FileHandler>();
serviceCollection.AddScoped<StoryDataDownloader>();
var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
	.AddJsonFile("appsettings.json")
	.Build();
serviceCollection.Configure<MarvelConfig>(configuration.GetSection("MarvelApi"));
serviceCollection.AddScoped<MarvelConfig>();
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
var storyDataDownloader = serviceProvider.GetRequiredService<StoryDataDownloader>();
await storyDataDownloader.Start();