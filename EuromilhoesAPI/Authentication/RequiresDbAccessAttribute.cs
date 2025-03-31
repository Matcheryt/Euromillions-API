using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EuromilhoesAPI.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresDbAccessAttribute : Attribute, IAuthorizationFilter
    {
        private readonly DatabaseAccess _dbAccess;

        public RequiresDbAccessAttribute(DatabaseAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = _dbAccess.HasAccess(context.HttpContext.Request.Query["token"].ToString());

            if (!hasClaim)
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
