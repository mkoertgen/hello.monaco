using contracts.Resources;

namespace contracts.Posts;

public class MockPosts : IProvidePosts
{
    private static readonly BlogPost[] Posts = typeof(MockPosts).Assembly.ReadJson<BlogPost[]>("Resources/sample-data/posts.json");

    public Task<IEnumerable<BlogPost>> GetPosts()
    {
        return Task.FromResult(Posts.AsEnumerable());
    }
}
