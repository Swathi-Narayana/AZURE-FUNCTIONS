using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Dynamic;
namespace http_pratice.CommonUtilities
{
    public static class StudentCosmologic
    {
        public static IActionResult GenerateResponse(bool success, object data, string message)
        {
            dynamic responseData = new ExpandoObject();
            responseData.success = success;
            responseData.message = message;
            responseData.Data = data;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
            return new OkObjectResult(json);
        }
    }
}

