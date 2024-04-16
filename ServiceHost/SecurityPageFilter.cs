﻿using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using _0_Framework.Application;

namespace ServiceHost
{
    public class SecurityPageFilter:IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission = 
                (NeedsPermissionAttribute)context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute));

            if (handlerPermission==null)
            {
                return;
            }

            var accountPermission = _authHelper.GetPermissions();

            if (!accountPermission.All(x=>x != handlerPermission.Permission ))
            {
                context.HttpContext.Response.Redirect("/Account");
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }
}
