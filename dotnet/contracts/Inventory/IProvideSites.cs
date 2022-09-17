namespace contracts.Inventory;

public interface IProvideSites : IProvide<Site>
{
    Site Get(int id);
    void Save(Site site);
}
