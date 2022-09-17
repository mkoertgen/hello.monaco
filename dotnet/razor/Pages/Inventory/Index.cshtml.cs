using contracts.Inventory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor.Pages.Inventory;

public class Index : PageModel
{
    private readonly IProvideSites _provider;
    private readonly List<Site> _sites = Enumerable.Empty<Site>().ToList();
    public IEnumerable<Site> Sites => _sites;

    public Index(IProvideSites provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public async Task OnGet()
    {
        _sites.Clear();
        _sites.AddRange(await _provider.GetAll());
    }
}
