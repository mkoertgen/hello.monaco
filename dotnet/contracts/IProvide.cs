namespace contracts;

public interface IProvide<T>
{
    Task<IEnumerable<T>> GetAll();
}
