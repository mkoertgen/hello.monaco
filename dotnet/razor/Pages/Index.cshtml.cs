using contracts.Posts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor.Pages;

public class IndexModel : PageModel
{
    private readonly IProvidePosts _provider;
    private readonly List<BlogPost> _posts = Enumerable.Empty<BlogPost>().ToList();

    public IndexModel(IProvidePosts provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public IEnumerable<BlogPost> Posts => _posts;

    public async Task OnGet()
    {
        _posts.Clear();
        _posts.AddRange(await _provider.GetPosts());
    }
}
