using contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor.Pages.Inventory;


public class Edit : PageModel
{
    private readonly IProvideSites _provider;
    private readonly ILogger _logger;

    public Edit(IProvideSites provider, ILogger<Edit> logger)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty] public Site Site { get; set; } = null!;
    public int Id => Site.Id;

    public IActionResult OnGet(int id)
    {
        try
        {
            Site = _provider.Get(id);
            return Page();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting site {Id}", id);
            return NotFound();
        }
    }

    public IActionResult OnPostSave()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _logger.LogInformation("Saving site config: {Id}", Site.Id);
        _provider.Save(Site);
        return Page();
    }
}
