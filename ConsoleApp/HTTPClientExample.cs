using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp;

public class HTTPClientExample
{
    private readonly HttpClient _httpClient;
    private readonly string _url = "https://jsonplaceholder.typicode.com/posts";

    public HTTPClientExample()
    {
        _httpClient = new HttpClient();
    }
    public async Task Read()
    {
        var response = await _httpClient.GetAsync(_url);

        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }

    public async Task Get(int id)
    {
        var response = await _httpClient.GetAsync($"{_url}/{id}");

        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
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

        var jsonContent = JsonConvert.SerializeObject(postModel);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_url, httpContent);
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }

    public async Task Update(int id, int userId, string title, string body)
    {
        var postModel = new PostModel
        {
            UserId = userId,
            Title = title,
            Body = body
        };

        var jsonContent = JsonConvert.SerializeObject(postModel);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_url}/{id}", httpContent);
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }

    public async Task Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_url}/{id}");
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return;
        }
        Console.WriteLine($"Error: {response.StatusCode}");
    }
    
}

public class PostModel
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
