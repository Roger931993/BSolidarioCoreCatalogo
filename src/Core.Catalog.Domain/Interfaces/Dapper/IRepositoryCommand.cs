using Core.Catalog.Domain.Common.Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
    public interface IRepositoryCommand<TDomainEntity> where TDomainEntity : Entity
    {
        Task<int> AddEntityAsync(TDomainEntity entity);
        Task<IEnumerable<TDomainEntity>> AddEntitiesAsync(IEnumerable<TDomainEntity> entities);
        Task<IEnumerable<TDomainEntity>> AddEntitiesWithKeyAsync(IEnumerable<TDomainEntity> entities);
        Task UpdateEntityAsync<TIdEntity>(TIdEntity tIdEntity, TDomainEntity entity);
        Task DeleteEntityAsync<TIdEntity>(TIdEntity tIdEntity);
        Task<IEnumerable<TDomaindEntity>> SelectEntitiesAsync<TDomaindEntity>(TDomaindEntity filterEntity);
        Task<IEnumerable<TDomaindEntity>> SelectEntitieByIdAsync<TDomaindEntity, TIdEntity>(TIdEntity tIdEntity);
    }
}
