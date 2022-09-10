namespace contracts.Posts;

public class JsonPosts : JsonProvider<BlogPost>, IProvidePosts
{
    public JsonPosts(HttpClient http, string uri="https://jsonplaceholder.typicode.com/posts"): base(http, uri)
    { }

    public async Task<IEnumerable<BlogPost>> GetPosts()
    {
        return await GetAll();
    }
}
