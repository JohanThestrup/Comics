namespace comics.MarvelApi.Models;

public record CharacterList
{
	public int Available { get; set; }
	public int Returned { get; set; }
	public string CollectionURI { get; set; }
	public CharacterSummary[] Items { get; set; }
}

public record CharacterSummary
{
	public string ResourceURI;
	public string Name;
	public string Role;
}

public record CharacterDataWrapper
{
	public int Code;
	public string Status;
	public string Copyright;
	public string AttributionText;
	public string AttributionHTML;
	public CharacterDataContainer Data;
	public string Etag;
}

public record CharacterDataContainer
{
	public int Offset;
	public int Limit;
	public int Total;
	public int Count;
	public Character[] Results;
}

public record Character
{
	public int Id;
	public string Name;
	public string Description;
	public DateTime Modified;
	public string ResourceURI;
	public Url[] URLs;
	public Image Thumbnail;
	public ComicList Comics;
	public StoryList Stories;
	public EventList Events;
	public SeriesList Series;
}