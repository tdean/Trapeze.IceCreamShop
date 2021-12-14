// <copyright file="HttpContextUserMiddleware.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware used to authenticate and add a user to the request.
    /// </summary>
    public class HttpContextUserMiddleware
    {
        private static Dictionary<string, string> _names = new Dictionary<string, string>(new List<KeyValuePair<string, string>>()
        {
          new KeyValuePair<string, string>("amosvani:TW9zdmFuaXh4", "Alanna Mosvani"),
          new KeyValuePair<string, string>("mcauthon:Q2F1dGhvbnh4", "Mat Cauthon"),
          new KeyValuePair<string, string>("mdamodred:RGFtb2RyZWR4", "Moiraine Damodred")
        });

        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextUserMiddleware"/> class.
        /// </summary>
        /// <param name="next">A <see cref="RequestDelegate"/> containing the next request delegate to process.</param>
        public HttpContextUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The invoke method used to forward the request.
        /// </summary>
        /// <param name="httpContext">A <see cref="HttpContext"/> containing the context of the request.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var authHeader = httpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic", StringComparison.InvariantCulture))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Invalid Authentication").ConfigureAwait(false);
                return;
            }

            var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            if (!_names.ContainsKey(usernamePassword))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Invalid Username & Password Combination").ConfigureAwait(false);
                return;
            }

            httpContext.User = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, _names[usernamePassword])
                    },
                    "TestAuthentication"));

            await _next(httpContext).ConfigureAwait(false);
        }
    }
}
