using contracts.Resources;

namespace contracts.Inventory;

public class MockSites : IProvideSites
{
    private static readonly IDictionary<int, Site> Sites = typeof(MockSites).Assembly
        .ReadJson<Site[]>("Resources/sample-data/sites.json")
        .ToDictionary(s => s.Id, s => s);

    public Task<IEnumerable<Site>> GetAll()
    {
        return Task.FromResult(Sites.Values.AsEnumerable());
    }

    public Site Get(int id) => Sites[id];
    public void Save(Site site) => Sites[site.Id] = site;
}
