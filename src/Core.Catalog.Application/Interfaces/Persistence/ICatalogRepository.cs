using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Catalog.Application.Interfaces.Persistence
{
    public interface ICatalogRepository
    {
        DbSet<T> GetMore<T>() where T : class;
        IQueryable<T> GetAsNoTracking<T>() where T : class;
        IQueryable<T> GetIncludesAsNoTraking<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task<T> Save<T>(T user) where T : class;
        Task<List<T>> SaveRange<T>(List<T> model) where T : class;
        Task<T> SaveUpdate<T>(int Id, T model) where T : class;
        Task<T> Update<T>(int Id, T model, List<string>? PropiedadesExcluir = null) where T : class;
        Task DeletePhysical<T>(int Id) where T : class;
        Task DeleteLogic<T>(int Id, int? deletedStatus = 0, string? NameFieldStatus = "status") where T : class;
        Task DeletePhysicalRange<T>(List<int> Ids) where T : class;
        Task DeleteLogicRange<T>(List<int> Ids, string nameId, int? deletedStatus = 0, string? NameFieldStatus = "status") where T : class;
        Task TruncateTable(string Table, string? Schema = "public");
    }
}
