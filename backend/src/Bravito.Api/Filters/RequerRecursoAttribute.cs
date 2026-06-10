using System;
using Microsoft.AspNetCore.Mvc;

namespace Bravito.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RequerRecursoAttribute : TypeFilterAttribute
    {
        public RequerRecursoAttribute(string recurso) : base(typeof(RequerRecursoFilter))
        {
            Arguments = new object[] { recurso };
        }
    }
}
