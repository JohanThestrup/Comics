namespace Comics.Marvel.Models;
public record CreatorList
(
	int Available,
	int Returned,
	string CollectionURI,
	CreatorSummary[] Items
);

public record CreatorSummary(string ResourceURI, string Name, string Role);