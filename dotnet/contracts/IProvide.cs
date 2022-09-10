namespace contracts.Posts;

public interface IProvide<T>
{
    Task<IEnumerable<T>> GetAll();
}
