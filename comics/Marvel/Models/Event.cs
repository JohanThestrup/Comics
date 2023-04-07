namespace comics.Marvel.Models;
public record EventList
(
	int Available,
	int Returned,
	string CollectionURI,
	EventSummary[] Items
);

public record EventSummary(string ResourceURI, string Name);