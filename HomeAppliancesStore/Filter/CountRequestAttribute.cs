using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Filter
{
    public class CountRequestAttribute : Attribute, IActionFilter
    {
        private static Dictionary<DateTime, HttpRequest> requestDictionary = new Dictionary<DateTime, HttpRequest>();
        private static int countOfCompletedRequest = 0;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            requestDictionary[DateTime.Now] = context.HttpContext.Request;
            countOfCompletedRequest++;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public static int GetCountRequest()
        {
            return countOfCompletedRequest;
        }

        public static Dictionary<DateTime, HttpRequest> GetMoreInformationAboutRequest()
        {
            return requestDictionary;
        }
    }
}
