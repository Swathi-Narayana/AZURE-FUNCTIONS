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
using Azure.Core;
using System.Net.Http;
using Newtonsoft.Json;
namespace http_pratice
{
    public class StudentHttpTriggers
    {
        private const string DatabaseName = DataBaseConst.DataBaseName;
        private const string CollectionName = DataBaseConst.CollectionName;
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;
        private readonly IStudentDomain _studentDomain;
        StudentCosmologic studentlogic = new StudentCosmologic();

        public StudentHttpTriggers(CosmosClient cosmosClient, IStudentDomain studentDomain)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("Student", "StudentInformation");
            _studentDomain = studentDomain;
        }

        [FunctionName(HttpFunction.GetAllStudent)]
        public async Task<IActionResult> GetAllStudentInfo(
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Get, Route = Routes.GetStudent)] HttpRequestMessage req,
                [CosmosDB(DatabaseName,CollectionName,Connection= "CosmosDbConnectionString" ,CreateIfNotExists = true)]
                System.Collections.Generic.IEnumerable<Student> documents, ILogger log)
        {
            log.LogInformation("Getting list of all students");

            try
            {
                return await _studentDomain.ReadItem(documents);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Invalid Http verb";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }


        [FunctionName(HttpFunction.GetByIdStudent)]
        public async Task<IActionResult> GetStudentById(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.GetStudentId)]
             HttpRequestMessage req, ILogger log, string id)
        {
            log.LogInformation($"Reading with ID: {id}");
            try
            {
                
                return await _studentDomain.ReadItemById(id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
           {
                string gmessage = "Invalid input params,Please check";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }

        [FunctionName(HttpFunction.PostStudent)]
        public async Task<IActionResult> PostStudentInfo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post",  Route = Routes.PostStudent)]
        HttpRequestMessage req, 
       ILogger log)
        {
           
            log.LogInformation("Creating student with ID id");
            string requestData = await req.Content.ReadAsStringAsync();
            var StudentData = JsonConvert.DeserializeObject<Student>(requestData);
            try
            {
                
                return await _studentDomain.CreateItem(StudentData, documentContainer);

            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                string gmessage = "Failed to add the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }

        }

        [FunctionName(HttpFunction.DeleteStudent)]
        public async Task<IActionResult> DeleteStudentInfo(
                [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Delete, Route = Routes.DeleteStudent)]
                HttpRequestMessage req, ILogger log, string id)
        {

            try
            {
                return await _studentDomain.DeleteItem(id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to delete the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }

        [FunctionName(HttpFunction.UpadteStudent)]
        public async Task<IActionResult> UpdateStudentInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, Httpverbs.Put, Route = Routes.UpdateStudent)] HttpRequestMessage req,
            ILogger log, string id)
        {
            string requestData = await req.Content.ReadAsStringAsync();
            var Studentdata = JsonConvert.DeserializeObject<Student>(requestData);
            log.LogInformation($"Update Student Info with ID: {id}");

            try
            {
                return await _studentDomain.UpdateItem(Studentdata, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string gmessage = "Failed to update the student";
                return studentlogic.GenerateBadResponse(gmessage);
            }
        }
    }

}

