using Core.Catalog.Application.DTOs.Base;
using MediatR;

namespace Core.Catalog.Application.Features.CatalogError.Commands
{
    public class LoadCacheCommand : RequestBase<LoadCacheRequest>, IRequest<ResponseBase<LoadCacheResponse>>
    {
    }
    public class LoadCacheRequest
    {

    }
    public class LoadCacheResponse
    {

    }

}
