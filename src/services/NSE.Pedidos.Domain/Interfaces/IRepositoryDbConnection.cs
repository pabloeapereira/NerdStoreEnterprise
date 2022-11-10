using System.Data.Common;

namespace NSE.Pedidos.Domain.Interfaces
{
    public interface IRepositoryDbConnection
    {
        DbConnection GetDbConnection();
    }
}