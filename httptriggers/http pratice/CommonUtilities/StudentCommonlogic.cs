using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
namespace http_pratice.CommonUtilities
{
    public class StudentCosmologic
    {
        public IActionResult GenerateResponse(bool success, object data, string message)
        {
            dynamic responseData = new ExpandoObject();
            responseData.success = success;
            responseData.message = message;
            responseData.Data = data;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
            return new OkObjectResult(json);
        }

        public IActionResult GenerateBadResponse(string message)
        {
            dynamic responseData = new ExpandoObject();
            responseData.success = false;
            responseData.message = message;
            responseData.Data = null;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
            return new BadRequestObjectResult(json);

        }
    }
}

