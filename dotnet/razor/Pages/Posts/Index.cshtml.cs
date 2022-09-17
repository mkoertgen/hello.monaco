using contracts.Posts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor.Pages.Posts;

public class Index : PageModel
{
    private readonly IProvidePosts _provider;
    private readonly List<BlogPost> _posts = Enumerable.Empty<BlogPost>().ToList();
    public IEnumerable<BlogPost> Posts => _posts;

    public Index(IProvidePosts provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public async Task OnGet()
    {
        _posts.Clear();
        _posts.AddRange(await _provider.GetPosts());
    }
}
