public interface IDbQuery
{
    IQueryable<T> GetAll<T>() where T : class;
    IQueryable<T> GetByCondition<T>(Func<T, bool> predicate) where T : class;
}