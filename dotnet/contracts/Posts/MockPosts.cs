namespace contracts.Posts;

public class MockPosts : IProvidePosts
{
    public Task<IEnumerable<BlogPost>> GetPosts()
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(i => new BlogPost(
            i, "Title", "Body")));
    }
}
