
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using http_pratice.CommonUtilities;
using http_pratice.CommonUtilities.Modles;
using http_pratice.Domain;

namespace http_pratice
{
    public class StudentHttpTriggers
    {

        private const string DatabaseName = DataBaseConst.DataBaseName;
        private const string CollectionName = DataBaseConst.CollectionName;
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;
        private readonly IStudentDomain _studentDomain;

        public StudentHttpTriggers(CosmosClient cosmosClient, IStudentDomain studentDomain)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("Student", "StudentInformation");
            _studentDomain = studentDomain;
        }

        [FunctionName(HttpFunction.GetAllStudent)]
        public async Task<IActionResult> GetAllStudentInfo(
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Get, Route = Routes.GetStudent)] HttpRequest req,
                [CosmosDB(DatabaseName,CollectionName,Connection= "CosmosDbConnectionString" ,CreateIfNotExists = true)]
                System.Collections.Generic.IEnumerable<Student> documents, ILogger log)
        {
            log.LogInformation("Getting list of all students");
            try
            {
                return await _studentDomain.ReadItem(req, documents);
            }
            catch (Exception e)
            {
                string gmessage = "Invalid Http verb";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
        }


        [FunctionName(HttpFunction.GetByIdStudent)]
        public async Task<IActionResult> GetStudentById(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.GetStudentId)]
             HttpRequest req, ILogger log, string id)
        {
            log.LogInformation($"Reading with ID: {id}");
            try
            {
                
                return await _studentDomain.ReadItemById(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Invalid input params,Please check";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
        }

        [FunctionName(HttpFunction.PostStudent)]
        public async Task<IActionResult> PostStudentInfo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post",  Route = Routes.PostStudent)]
        HttpRequest req, [CosmosDB(DatabaseName, CollectionName, Connection = "CosmosDbConnectionString")]
        IAsyncCollector<dynamic> documentsOut, ILogger log, string id)
        {
           
            log.LogInformation($"Creating student with ID: {id}");
            try
            {
                
                return await _studentDomain.CreateItem(req, id, documentsOut);
            }
            catch (CosmosException e)
            {

                string gmessage = "Failed to add the student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
        }

        [FunctionName(HttpFunction.DeleteStudent)]
        public async Task<IActionResult> DeleteStudentInfo(
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Delete, Route = Routes.DeleteStudent)]
                HttpRequest req, ILogger log, string id)
        {
            try
            {
                return await _studentDomain.DeleteItem(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to delete the student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
        }

        [FunctionName(HttpFunction.UpadteStudent)]
        public async Task<IActionResult> UpdateStudentInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Put, Route = Routes.UpdateStudent)] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation($"Update Student Info with ID: {id}");

            try
            {
                return await _studentDomain.UpdateItem(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to update the student";
                return StudentCosmologic.GenerateResponse(false, null, gmessage);
            }
        }
    }

}

