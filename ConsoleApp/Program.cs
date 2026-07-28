using ConsoleApp;
using Newtonsoft.Json;

HTTPClientExample httpClient = new HTTPClientExample();
// await httpClient.Create(1, "New Post", "This is a new post created using HttpClient.");
// await httpClient.Get(101);
// await httpClient.Update(1, 1, "Updated Post", "This is an updated post.");
await httpClient.Read();