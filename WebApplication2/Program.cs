var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/jokes", async (IHttpClientFactory factory) =>
{
    var chuckNorrisJokeClient = factory.CreateClient("chuck");
    var dadJokeClient = factory.CreateClient("dad");
    dadJokeClient.DefaultRequestHeaders.Add("Accept", "application/json");

    var chuckNorrisJokeTask = chuckNorrisJokeClient.GetFromJsonAsync<ChuckNorrisJoke>("https://api.chucknorris.io/jokes/random?category=dev");
    var dadJokeTask = dadJokeClient.GetFromJsonAsync<DadJoke>("https://icanhazdadjoke.com/");

    await Task.WhenAll(chuckNorrisJokeTask, dadJokeTask);

    return new
    {
        chuckNorrisJoke = chuckNorrisJokeTask.Result,
        dadJoke = dadJokeTask.Result,
    };
});

app.Run();

internal record ChuckNorrisJoke
{
    public string id { get; set; }
    public string value { get; set; }
}

internal record DadJoke
{
    public string id { get; set; }
    public string joke { get; set; }
}