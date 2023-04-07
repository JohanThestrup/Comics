namespace comics.Marvel.Models;
public record ComicList
(
	int Available,
	int Returned,
	string CollectionURI,
	ComicSummary[] Items
);

public record ComicSummary(string ResourceURI, string Name);