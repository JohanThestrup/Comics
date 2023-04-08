namespace Comics.Marvel.Models;
public record CharacterList
(
	int Available,
	int Returned,
	string CollectionURI,
	CharacterSummary[] Items
);

public record CharacterSummary(string ResourceURI, string Name, string Role);

public record CharacterDataWrapper
(
	int Code,
	string Status,
	string Copyright,
	string AttributionText,
	string AttributionHTML,
	CharacterDataContainer Data,
	string Etag
);

public record CharacterDataContainer
(
	int Offset,
	int Limit,
	int Total,
	int Count,
	Character[] Results
);

public record Character
(
	int Id,
	string Name,
	string Description,
	DateTime Modified,
	string ResourceURI,
	Url[] URLs,
	Image Thumbnail,
	ComicList Comics,
	StoryList Stories,
	EventList Events,
	SeriesList Series
);