namespace Comics.Marvel.Models;
public record StoryDataWrapper
(
	int Code,
	string Status,
	string Copyright,
	string AttributionText,
	string AttributionHTML,
	StoryDataContainer Data,
	string Etag
);

public record StoryDataContainer
(
	int Offset,
	int Limit,
	int Total,
	int Count,
	List<Story> Results
);

public record StoryList
(
	int Available,
	int Returned,
	string CollectionURI,
	StorySummary[] Items
);

public record StorySummary(string ResourceURI, string Name, string Type);

public record Story
(
	int Id,
	string Title,
	string Description,
	string ResourceURI,
	string Type,
	DateTime Modified,
	Image Thumbnail,
	ComicList Comics,
	SeriesList Series,
	EventList Events,
	CharacterList Characters,
	CreatorList Creators,
	ComicSummary OriginalIssue
)
{
	public string Description { get; set; } = Description;
}