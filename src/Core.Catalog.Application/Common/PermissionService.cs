using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.Interfaces;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Microsoft.Extensions.Configuration;

namespace Core.Catalog.Application.Common
{
    public class PermissionService : IPermissionService
    {
        private readonly IRedisCache _redisCache;
        private readonly IConfiguration _configuration;
     

        public PermissionService(IRedisCache redisCache, IConfiguration configuration)
        {
            _redisCache = redisCache;
            this._configuration = configuration;            
        }

        public async Task<bool> HasPermissionAsync(string idSession, string Verb, string permission)
        {
            bool blnFlagAuth = _configuration["AuthenticationSettings:UseNoValidation"] == "True" ? true : false;
            if (blnFlagAuth)
                return true;

            if (string.IsNullOrEmpty(idSession))
                return false;

            List<adm_permissionDto> userPermissions = await _redisCache.GetAsync<List<adm_permissionDto>>($"{idSession}-adm_permission_backend");
            if (userPermissions == null)
                return false;

            return userPermissions.Any(x => x.action == Verb && x.action == permission);
        }
    }
}
