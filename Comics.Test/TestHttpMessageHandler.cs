namespace Comics.Tests;
public class TestHttpMessageHandler : HttpMessageHandler
{
	private readonly HttpResponseMessage response;

	public TestHttpMessageHandler(HttpResponseMessage response)
	{
		this.response = response;
	}

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return Task.FromResult(response);
	}
}