using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Middleware
{
    public class RequestMiddleware
    {
        private static List<string> NameUsers = new List<string>();
        private static int countAuthenticatedRequest = 0;
        private readonly RequestDelegate next;

        public RequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.User;
            if (token.Identity.IsAuthenticated)
            {
                NameUsers.Add(token.Identity.Name);
                countAuthenticatedRequest++;
                await this.next.Invoke(context);
            }
            else
            {
                await this.next.Invoke(context);
            }
        }

        public static List<string> GetListAuthenticatedUsers
        {
            get
            {
                return NameUsers;
            }
        }

        public static int GetCountAuthenticatedRequest
        {
            get
            {
                return countAuthenticatedRequest;
            }
        }
    }
}
