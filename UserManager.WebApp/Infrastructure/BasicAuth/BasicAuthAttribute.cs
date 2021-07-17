using Microsoft.AspNetCore.Mvc;
using System;
using UserManager.Core.Infrastructure.BasicAuth;

namespace UserManager.WebApp.Infrastructure.BasicAuth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute(string realm = @"Auth") : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { realm };
        }
    }
}