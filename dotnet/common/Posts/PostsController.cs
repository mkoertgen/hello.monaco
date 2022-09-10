using Microsoft.AspNetCore.Mvc;

namespace contracts.Posts;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IProvidePosts _service;

    public PostsController(IProvidePosts service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IEnumerable<BlogPost>> GetAsync()
    {
        return await _service.GetPosts();
    }
}
