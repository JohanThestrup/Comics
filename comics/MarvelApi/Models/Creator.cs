namespace comics.MarvelApi.Models;

public record CreatorList
{
	public int Available { get; set; }
	public int Returned { get; set; }
	public string CollectionURI { get; set; }
	public CreatorSummary[] Items { get; set; }
}

public record CreatorSummary
{
	public string ResourceURI { get; set; }
	public string Name { get; set; }
	public string Role { get; set; }
}