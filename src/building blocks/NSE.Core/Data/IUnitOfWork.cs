namespace NSE.Core.Data
{
    public interface IUnitOfWork
    {
        ValueTask<bool> CommitAsync();
    }
}