namespace comics.MarvelApi.Models;

public record EventList
{
	public int Available { get; set; }
	public int Returned { get; set; }
	public string CollectionURI { get; set; }
	public EventSummary[] Items { get; set; }
}

public record EventSummary
{
	public string ResourceURI { get; set; }
	public string name { get; set; }
}