using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using System.Dynamic;
using System.Collections.Generic;
using Newtonsoft.Json;
using http_pratice.NewFolder;

namespace http_pratice
{

    public static class Function1
    {
        private const string DatabaseName = "Student";
        private const string CollectionName = "StudentInformation";
       


        [FunctionName("GetInfo")]

        public static async Task<IActionResult> GetInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getallstudentinfo")] HttpRequest req,
             [CosmosDB(DatabaseName,CollectionName,Connection ="CosmosDBConnectionString",
            SqlQuery = "SELECT * FROM c")]
             IEnumerable<dynamic> documents,
        ILogger log)
        {
            log.LogInformation("Records are fectings");
            string respondmessage = "Retrived all students information sucessfull";
            dynamic getmydata = new ExpandoObject();
            getmydata.message = respondmessage;
            getmydata.Data = documents;
            string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(getmydata);
            log.LogInformation("successfully");
            return new OkObjectResult(jsondata);
        }
    }
    [FunctionName("CreateShoppingCartItem")]
    public async Task<IActionResult> CreateShoppingCartItems(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createshoppingcartitem")] HttpRequest req,
   ILogger log)
    {
        log.LogInformation("Creating Shopping Cart Item");
        string requestData = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<Student>(requestData);

        var validator = new CreateShoppingCartItemValidator();
        var validationResult = await validator.ValidateAsync(data);

        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors);
        }

        var item = new Student
        {

            Name = data.Name,
            Age = data.Age,
            DOB = data.DOB,
            Phone = data.Phone,
            Email = data.Email,
        };

        await documentContainer.CreateItemAsync(item, new Microsoft.Azure.Cosmos.PartitionKey(item.Category));
        string responsemessage = "Created an item successfully";
        dynamic cmydata = new ExpandoObject();
        cmydata.message = responsemessage;
        cmydata.Data = item;
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(cmydata);
        return new OkObjectResult(json);
    }
}
