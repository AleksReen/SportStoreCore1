using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest httpRequest)
        {
            var result = httpRequest.QueryString.HasValue ? $"{httpRequest.Path} {httpRequest.QueryString}" : httpRequest.Path.ToString();
            return result;
        }       
    }
}
