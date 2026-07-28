using System;
using RestSharp;

namespace ConsoleApp;

public class RestClientExample
{
    private readonly RestClient _restClient;
    private readonly string _url = "https://jsonplaceholder.typicode.com/posts";

    public RestClientExample()
    {
        _restClient = new RestClient();
    }

    public async Task Read()
    {
        var request = new RestRequest(_url, Method.Get);
        var response = await _restClient.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    public async Task Get(int id)
    {
        var request = new RestRequest($"{_url}/{id}", Method.Get);
        var response = await _restClient.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
            return;
        }

        Console.WriteLine($"Error: {response.StatusCode}");
    }

    public async Task Create(int userId, string title, string body)
    {
        var postModel = new PostModel
        {
            UserId = userId,
            Title = title,
            Body = body
        };

        var request = new RestRequest(_url, Method.Post);
        request.AddJsonBody(postModel);

        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }

    public async Task Update(int id, int userId, string title, string body)
    {
        var postModel = new PostModel
        {
            UserId = userId,
            Id = id,
            Title = title,
            Body = body
        };

        var request = new RestRequest($"{_url}/{id}", Method.Put);
        request.AddJsonBody(postModel);

        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }

    public async Task Delete(int id)
    {
        var request = new RestRequest($"{_url}/{id}", Method.Delete);
        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine(response.Content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }
}