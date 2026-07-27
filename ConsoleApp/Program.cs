using Newtonsoft.Json;



string json2 = """{"Id":1,"Title":"My First Blog","Author":"John Doe","Content":"This is the content of my first blog."}""";

var blog2 = JsonConvert.DeserializeObject<BlogModel>(json2);
Console.WriteLine(blog2.Author);

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}


public static class Extensions
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}