using contracts.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace contracts.Sites;

[ApiController]
[Route("api/[controller]")]
public class SitesController : ControllerBase
{
    private readonly IProvideSites _service;

    public SitesController(IProvideSites service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IEnumerable<Site>> GetAllAsync()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id:int}")]
    public Site Get(int id)
    {
        return _service.Get(id);
    }
}
