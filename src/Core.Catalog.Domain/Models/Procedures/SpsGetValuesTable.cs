using Core.Catalog.Domain.Common.Dapper;
using System.Data;

namespace Core.Catalog.Domain.Models.Procedures
{
    [DataAnnotationSpName("sps_find_values_table")]
    public class SpsGetValuesTable: EntitySp
    {
        [SpParametersCommon("@tabla_name", DbType.String, ParameterDirection.Input)]
        public string? tabla_name { get; set; }
        [SpParametersCommon("@top", DbType.String, ParameterDirection.Input)]
        public string? top { get; set; }
        [SpParametersCommon("@conditions", DbType.String, ParameterDirection.Input)]
        public string? conditions { get; set; }
    }
}
