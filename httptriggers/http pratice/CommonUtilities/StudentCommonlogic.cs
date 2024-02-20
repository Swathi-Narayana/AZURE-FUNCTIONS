using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
namespace http_pratice.CommonUtilities
{
<<<<<<< HEAD
    public class StudentCosmologic
    {
        public IActionResult GenerateResponse(bool success, object data, string message)
=======
    public  class StudentCosmologic
    {
        public  IActionResult GenerateResponse(bool success, object data, string message)
>>>>>>> 645198d923440a218678b613b60545179a158961
        {
            dynamic responseData = new ExpandoObject();
            responseData.success = success;
            responseData.message = message;
            responseData.Data = data;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(responseData);
            return new OkObjectResult(json);
        }

<<<<<<< HEAD
        public IActionResult GenerateBadResponse(string message)
=======
        public  IActionResult GenerateBadResponse(string message)
>>>>>>> 645198d923440a218678b613b60545179a158961
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

