namespace contracts.Posts;

public interface IProvidePosts
{
    Task<IEnumerable<BlogPost>> GetPosts();
}
