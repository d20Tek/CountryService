using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace D20Tek.CountryService.UnitTests
{
    internal static class HttpResultHelper
    {
        public static int? GetStatusCode<T>(ActionResult<T> actionResult)
        {
            var httpResult = actionResult.Result as ObjectResult;
            Assert.IsNotNull(httpResult);
            return httpResult.StatusCode;
        }
    }
}
