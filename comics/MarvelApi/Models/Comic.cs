namespace comics.MarvelApi.Models;

public record ComicList
{
	public int Available { get; set; }
	public int Returned { get; set; }
	public string CollectionURI { get; set; }
	public ComicSummary[] Items { get; set; }
}

public record ComicSummary
{
	public string ResourceURI { get; set; }
	public string Name { get; set; }
}