using Core.Catalog.Application.DTOs.Base;

namespace Core.Catalog.Application.Interfaces.Base
{
    public interface IRequestBase
    {
        Guid? IdTraking { get; set; }
        InfoSessionDto? InfoSession { get; set; }
    }
}
