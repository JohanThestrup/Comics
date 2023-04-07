namespace comics.Marvel.Models;
public record SeriesList
(
	int Available,
	int Returned,
	string CollectionURI,
	ComicSummary[] Items
);

