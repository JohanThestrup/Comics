namespace comics.MarvelApi.Models;

public record SeriesList
{
	public int Available { get; set; }
	public int Returned { get; set; }
	public string CollectionURI { get; set; }
	public ComicSummary[] Items { get; set; }
}