namespace comics.MarvelApi.Models;

public record StoryDataWrapper
{
	public int Code { get; set; }
	public string Status { get; set; }
	public string Copyright { get; set; }
	public string AttributionText { get; set; }
	public string AttributionHTML { get; set; }
	public StoryDataContainer Data { get; set; }
	public string Etag { get; set; }
}

public record StoryDataContainer
{
	public int Offset { get; set; }
	public int Limit { get; set; }
	public int Total { get; set; }
	public int Count { get; set; }
	public List<Story> Results { get; set; }
}

public record StoryList
{
	public int Available;
	public int Returned;
	public string CollectionURI;
	public StorySummary[] Items;
}

public record StorySummary
{
	public string ResourceURI;
	public string Name;
	public string Type;
}
public record Story
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string resourceURI { get; set; }
	public string Type { get; set; }
	public DateTime Modified { get; set; }
	public Image Thumbnail { get; set; }
	public ComicList Comics { get; set; }
	public SeriesList Series { get; set; }
	public EventList Events { get; set; }
	public CharacterList Characters { get; set; }
	public CreatorList Creators { get; set; }
	public ComicSummary OriginalIssue { get; set; }
}

